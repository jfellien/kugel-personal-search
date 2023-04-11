import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { PersonComponent } from './person/person.component';
import { SearchComponent } from './search/search.component';
import { StartComponent } from './start/start.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: StartComponent,
    canActivate: [ MsalGuard ]
  },
  {
    path: 'search',
    component: SearchComponent,
    canActivate: [ MsalGuard ]
  },
  {
    path: 'person/:personId',
    component: PersonComponent,
    canActivate: [ MsalGuard ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
