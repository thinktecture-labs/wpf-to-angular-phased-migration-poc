import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IComponentSample {
  id: string;
  componentName: string;
  migrationTime: string;
  peakArea: number;
}

@Injectable({
  providedIn: 'root'
})
export class ComponentSampleService {

  constructor(private httpClient: HttpClient) { }

  getComponentSample(id: string): Observable<IComponentSample> {
    return this.httpClient.get<IComponentSample>('http://localhost:5000/api/componentSamples/' + id);
  }

  deleteComponentSample(id: string): Observable<Object> {
    return this.httpClient.delete('http://localhost:5000/api/componentSamples/' + id);
  }
}
