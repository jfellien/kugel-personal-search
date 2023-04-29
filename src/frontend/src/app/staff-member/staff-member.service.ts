import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StaffMember } from '../models/staff-member.model';

@Injectable({
  providedIn: 'root'
})
export class StaffMemberService {

  query: string | undefined;

  constructor(private http: HttpClient) { }

  get(id: string) {
    return this.http.get<StaffMember>(`${environment.endpoint}/staff-members/${id}`);
  }
}
