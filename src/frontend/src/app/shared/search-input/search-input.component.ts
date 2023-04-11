import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-input',
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.scss']
})
export class SearchInputComponent {

  @Input() query: string | undefined;

  @Output() submitSearch = new EventEmitter<string>();

  constructor(private router: Router) { }

  queryChanged(evt: KeyboardEvent) {
    if (evt.key === 'Enter') {
      this.search();
    }
  }

  search() {
    this.router.navigate(['search'], { queryParams: {
      q: this.query
    } });
    this.submitSearch.emit(this.query);
  }

}
