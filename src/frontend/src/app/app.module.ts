import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MsalInterceptor, MsalModule, MsalRedirectComponent } from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { environment } from 'src/environments/environment';
import { StartComponent } from './start/start.component';
import { SearchComponent } from './search/search.component';
import { MaterialModule } from './shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogoComponent } from './shared/logo/logo.component';
import { SearchInputComponent } from './shared/search-input/search-input.component';
import { AccountMenuComponent } from './shared/account/account-menu/account-menu.component';
import { SelectorComponent } from './shared/selector/selector.component';
import { ErrorComponent } from './shared/error/error.component';
import { BytePipe } from './shared/byte.pipe';
import { LanguagePipe } from './shared/language.pipe';
import { PersonComponent } from './person/person.component';

@NgModule({
  declarations: [
    AppComponent,
    StartComponent,
    SearchComponent,
    LogoComponent,
    SearchInputComponent,
    AccountMenuComponent,
    SelectorComponent,
    ErrorComponent,
    BytePipe,
    LanguagePipe,
    PersonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MsalModule.forRoot(
      new PublicClientApplication({
        auth: {
          clientId: environment.clientId,
          authority: environment.authority,
          redirectUri: environment.redirectUri,
          knownAuthorities: [ environment.authorityDomain ]
        },
        cache: {
          cacheLocation: 'localStorage',
          storeAuthStateInCookie: false
        }
      }),
      {
        interactionType: InteractionType.Redirect,
        authRequest: {
          scopes: [ 'openid' ]
        }
      },
      {
        interactionType: InteractionType.Redirect,
        protectedResourceMap: new Map([
          [ `${environment.endpoint}${environment.pathSearch}`, environment.endpointScopes ],
          [ `${environment.endpoint}${environment.pathProducts}`, environment.endpointScopes ],
          [ `${environment.endpoint}${environment.pathSoftware}`, environment.endpointScopes ],
        ])
      }
    )
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true }
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
