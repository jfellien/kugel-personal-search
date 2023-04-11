import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SearchResult } from '../models/search-result.model';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  results: SearchResult[] | undefined;
  isLoading = false;
  errorMessage: string | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public searchService: SearchService
  ) {}

  ngOnInit(): void {
    if (this.route.snapshot.queryParams.q) {
      this.search(this.route.snapshot.queryParams.q);
    } else {
      this.router.navigate(['']);
    }
  }

  search(query: string) {
    this.errorMessage = undefined;
    this.isLoading = true;
    this.searchService.search(query).subscribe(result => {
      this.results = result;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
      this.errorMessage = 'Suche fehlgeschlagen. Keine Verbindug zum Server. Bitte versuchen Sie es später wieder.';
    });
  }
}
