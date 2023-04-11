import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SearchResult } from '../models/search-result.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  query: string | undefined;

  constructor(private http: HttpClient) { }

  search(query: string) {
    this.query = query;
    return this.http.get<SearchResult[]>(`${environment.endpoint}/search?q=${query}&skip=0&top=1000`);
  }
}
