import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Option } from '../../models/option.model';

@Injectable({
  providedIn: 'root'
})
export class SelectorService {

  constructor(private http: HttpClient) { }

  get(path: string) {
    return this.http.get<Option[]>(`${environment.endpoint}/${path}`);
  }
}
