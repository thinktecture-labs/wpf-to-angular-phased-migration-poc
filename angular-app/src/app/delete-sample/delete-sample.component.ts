import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ComponentSampleService, IComponentSample } from '../component-sample.service';

@Component({
  selector: 'app-delete-sample',
  templateUrl: './delete-sample.component.html',
  styleUrls: ['./delete-sample.component.scss']
})
export class DeleteSampleComponent implements OnInit {

  componentSample$!: Observable<IComponentSample>;

  constructor(
    private route: ActivatedRoute,
    private service: ComponentSampleService
  ) { }

  ngOnInit(): void {
    this.componentSample$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.service.getComponentSample(params.get('id')!))
    );
  }

  onCancelClicked(): void {

  }

  onDeleteClicked(): void {

  }
}
