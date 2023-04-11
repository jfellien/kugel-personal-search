import { Component, OnDestroy, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import {
  EventMessage,
  EventType
} from '@azure/msal-browser';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-account-menu',
  templateUrl: './account-menu.component.html',
  styleUrls: ['./account-menu.component.scss'],
})
export class AccountMenuComponent implements OnInit, OnDestroy {

  isLoggedIn = false;
  username: string | undefined;
  displayname: string | undefined;
  private readonly destroying$ = new Subject<void>();

  constructor(
    private msalService: MsalService,
    private msalBroadcastService: MsalBroadcastService
  ) {}

  ngOnInit() {
    this.handleLogin();
    this.msalBroadcastService.msalSubject$
      .pipe(
        takeUntil(this.destroying$),
        filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS)
      )
      .subscribe((result: EventMessage) => {
        this.handleLogin();
      });
  }

  ngOnDestroy(): void {
    this.destroying$.next(undefined);
    this.destroying$.complete();
  }

  handleLogin() {
    this.isLoggedIn = this.msalService.instance.getAllAccounts().length > 0;
    if (this.isLoggedIn) {
      const account = this.msalService.instance.getAllAccounts()[0];
      this.username = account.username;
      this.displayname = account.name;
    }
  }

  login() {
    this.msalService.instance.loginRedirect();
  }

  logout() {
    this.msalService.logout();
  }
}
