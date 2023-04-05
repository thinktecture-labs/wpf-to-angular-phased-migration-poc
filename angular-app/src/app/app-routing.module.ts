import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeleteSampleComponent } from './delete-sample/delete-sample.component';
import { ComponentListComponent } from './component-list/component-list.component';

const routes: Routes = [
  { path: 'componentSamples/delete/:id', component: DeleteSampleComponent },
  { path: 'componentSamples', component: ComponentListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
