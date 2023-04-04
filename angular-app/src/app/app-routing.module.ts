import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeleteSampleComponent } from './delete-sample/delete-sample.component';

const routes: Routes = [
  { path: 'componentSamples/delete', component: DeleteSampleComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
