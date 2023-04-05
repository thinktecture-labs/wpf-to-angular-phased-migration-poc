import { AfterViewInit, Component, NgZone, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ComponentSampleService, IComponentSample } from '../component-sample.service';
import { MatSelectionListChange } from '@angular/material/list';
import { Subject, filter, map, pairwise, takeUntil } from 'rxjs';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';

interface ICefSharp {
  BindObjectAsync(name: string): Promise<any>;
}

declare let CefSharp: ICefSharp;

interface ISamplesListBoundObject {
  registerSetSearchTerm(setSearchTerm: (searchTerm: string) => void): void;
  registerReload(reload: () => void): void;
  selectedSampleChanged(guid: string): void;
}

declare let samplesListBoundObject: ISamplesListBoundObject;

@Component({
  selector: 'app-component-list',
  templateUrl: './component-list.component.html',
  styleUrls: ['./component-list.component.scss']
})
export class ComponentListComponent implements OnInit, OnDestroy, AfterViewInit {

  @ViewChild('viewport') viewport!: CdkVirtualScrollViewport;

  componentSamples?: IComponentSample[];
  private readonly take = 30;
  private searchTerm: string = '';
  private currentCancellationSubject$?: Subject<void>;
  private isAtEnd: boolean = false;

  constructor(
    private componentSampleService: ComponentSampleService,
    private ngZone: NgZone) { }

  ngOnInit(): void {
    this.tryBindToWpfHost();
    this.load();
  }

  ngAfterViewInit(): void {

    const heightOfTwoElements = 140;

    this.viewport
      .elementScrolled()
      .pipe(
        map(() => this.viewport.measureScrollOffset('bottom')),
        pairwise(),
        filter(([y1, y2]) => (y2 < y1 && y2 < heightOfTwoElements)),
      )
      .subscribe(() =>
        this.load()
      );
  }

  private async tryBindToWpfHost(): Promise<any> {
    if (typeof (CefSharp) === "undefined")
      return;

    await CefSharp.BindObjectAsync('samplesListBoundObject');
    samplesListBoundObject.registerSetSearchTerm(this.setSearchTerm);
    samplesListBoundObject.registerReload(this.reload);
  }

  onSelectionChanged(event: MatSelectionListChange) {
    if (typeof (samplesListBoundObject) === 'undefined' || !event.options)
      return;

    if (event.options.length === 0) {
      samplesListBoundObject.selectedSampleChanged("");
      return;
    }

    const value = event.options[0].value as IComponentSample;
    samplesListBoundObject.selectedSampleChanged(value.id);
  }

  private setSearchTerm(searchTerm: string): void {
    if (searchTerm === this.searchTerm)
      return;

    this.searchTerm = searchTerm;
    this.reload();
  }

  private reload(): void {
    this.ngZone.run(() => {
      this.componentSamples = undefined;
    });
    this.cancelOngoingRequestIfNecessary();
    this.isAtEnd = false;
    this.load();
  }

  private load() {

    if (this.currentCancellationSubject$ || this.isAtEnd)
      return;

    const cancellationSubjectForThisRequest = new Subject<void>();
    this.currentCancellationSubject$ = cancellationSubjectForThisRequest;
    var skip = this.componentSamples?.length ?? 0;
    this.componentSampleService
      .getComponentSamples(skip, this.take, this.searchTerm)
      .pipe(takeUntil(this.currentCancellationSubject$))
      .subscribe((loadedSamples) => {

        if (loadedSamples.length > 0) {
          this.ngZone.run(() => {
            if (!this.componentSamples)
              this.componentSamples = loadedSamples;
            else
              this.componentSamples = [...this.componentSamples, ...loadedSamples];
          });
        }

        if (loadedSamples.length < this.take)
          this.isAtEnd = true;

        cancellationSubjectForThisRequest.complete();
        this.currentCancellationSubject$ = undefined;
      });
  }

  ngOnDestroy(): void {
    this.cancelOngoingRequestIfNecessary();
  }

  private cancelOngoingRequestIfNecessary(): void {
    if (this.currentCancellationSubject$) {
      this.currentCancellationSubject$.next();
      this.currentCancellationSubject$.complete();
      this.currentCancellationSubject$ = undefined;
    }
  }
}
