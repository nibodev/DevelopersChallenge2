import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { ListOfxComponent } from './components/list-ofx/list-ofx.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UploadOfxComponent } from './components/upload-ofx/upload-ofx.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'upload-ofx', component: UploadOfxComponent },
  { path: 'list-ofx', component: ListOfxComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
