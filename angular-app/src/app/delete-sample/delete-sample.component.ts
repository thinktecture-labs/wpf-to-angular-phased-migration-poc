import { Component } from '@angular/core';

interface IComponentSample {
  id: string;
  componentName: string;
  migrationTime: string;
  peakArea: number;
}

@Component({
  selector: 'app-delete-sample',
  templateUrl: './delete-sample.component.html',
  styleUrls: ['./delete-sample.component.scss']
})
export class DeleteSampleComponent {
  componentSample: IComponentSample;

  constructor() {
    this.componentSample = {
      id: "9208313f-d222-444b-8ae4-9811116efb52",
      componentName: "Nicotine (C10H14N2)",
      migrationTime: "00:20:13",
      peakArea: 915388
    };
  }

  onCancelClicked(): void {

  }

  onDeleteClicked(): void {

  }
}
