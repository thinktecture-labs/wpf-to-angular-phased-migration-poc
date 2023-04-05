import { Component, OnDestroy, OnInit } from '@angular/core';
import { ComponentSampleService, IComponentSample } from '../component-sample.service';
import { MatSelectionListChange } from '@angular/material/list';
import { Subject, takeUntil } from 'rxjs';

interface ICefSharp {
  BindObjectAsync(name: string): Promise<any>;
}

declare let CefSharp: ICefSharp;

interface ISamplesListBoundObject {
  registerSetSearchTerm(setSearchTerm: (searchTerm: string) => void): void;
  selectedSampleChanged(guid: string): void;
}

declare let samplesListBoundObject: ISamplesListBoundObject;

@Component({
  selector: 'app-component-list',
  templateUrl: './component-list.component.html',
  styleUrls: ['./component-list.component.scss']
})
export class ComponentListComponent implements OnInit, OnDestroy {

  componentSamples?: IComponentSample[];
  private readonly take = 30;
  private searchTerm: string = "";
  private currentCancellationSubject$?: Subject<void>;

  constructor(private componentSampleService: ComponentSampleService) { }


  ngOnInit(): void {
    this.tryBindToWpfHost();
    this.load();
  }

  private async tryBindToWpfHost(): Promise<any> {
    if (typeof (CefSharp) === "undefined")
      return;

    await CefSharp.BindObjectAsync('samplesListBoundObject');
    samplesListBoundObject.registerSetSearchTerm(this.setSearchTerm);
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
    if (searchTerm !== this.searchTerm)
      this.load();
  }

  private load() {

    this.cleanUpCancellationSubjectIfNecessary();

    const cancellationSubjectForThisRequest = new Subject<void>();
    this.currentCancellationSubject$ = cancellationSubjectForThisRequest;
    var skip = this.componentSamples?.length ?? 0;
    this.componentSampleService
      .getComponentSamples(skip, this.take, this.searchTerm)
      .pipe(takeUntil(this.currentCancellationSubject$))
      .subscribe((componentSamples) => {
        if (!this.componentSamples)
          this.componentSamples = componentSamples;
        else
          this.componentSamples.push(...componentSamples);

        if (Object.is(this.currentCancellationSubject$, cancellationSubjectForThisRequest)) {
          cancellationSubjectForThisRequest.complete();
          this.currentCancellationSubject$ = undefined;
        }
      });
  }

  ngOnDestroy(): void {
    this.cleanUpCancellationSubjectIfNecessary();
  }

  private cleanUpCancellationSubjectIfNecessary(): void {
    if (this.currentCancellationSubject$) {
      this.currentCancellationSubject$.next();
      this.currentCancellationSubject$.complete();
      this.currentCancellationSubject$ = undefined;
    }
  }
}
