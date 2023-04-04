import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ComponentSampleService, IComponentSample } from '../component-sample.service';

interface ICefSharp {
  BindObjectAsync(name: string): Promise<any>;
}

declare let CefSharp: ICefSharp;

interface IConfirmDeletionBoundObject {
  closeDialog(message: string, level: number): void;
}

declare let confirmDeletionBoundObject: IConfirmDeletionBoundObject;


@Component({
  selector: 'app-delete-sample',
  templateUrl: './delete-sample.component.html',
  styleUrls: ['./delete-sample.component.scss']
})
export class DeleteSampleComponent implements OnInit {

  componentSample$!: Observable<IComponentSample>;
  private readonly componentSampleId: string;

  constructor(route: ActivatedRoute, private service: ComponentSampleService) {
    this.componentSampleId = route.snapshot.paramMap.get('id')!;
  }

  ngOnInit(): void {
    this.componentSample$ = this.service.getComponentSample(this.componentSampleId);
  }

  onCancelClicked(): void {
    this.tryNotifyHost("", 0);
  }

  onDeleteClicked(componentName: string): void {
    this.service
      .deleteComponentSample(this.componentSampleId)
      .subscribe({
        next: () => this.tryNotifyHost(`${componentName} was deleted successfully`, 1),
        error: () => this.tryNotifyHost(`${componentName} could not be deleted`, 2)
      });
  }

  private async tryNotifyHost(message: string, level: number) {
    if (typeof(CefSharp) === "undefined")
      return;

    await CefSharp.BindObjectAsync("confirmDeletionBoundObject");

    confirmDeletionBoundObject.closeDialog(message, level);
  }
}
