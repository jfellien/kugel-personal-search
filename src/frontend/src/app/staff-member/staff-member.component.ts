import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StaffMember } from '../models/staff-member.model';
import { SearchService } from '../search/search.service';
import { StaffMemberService } from './staff-member.service';

@Component({
  selector: 'app-staff-member',
  templateUrl: './staff-member.component.html',
  styleUrls: ['./staff-member.component.scss']
})
export class StaffMemberComponent implements OnInit {

  isLoading = false;
  errorMessage: string | undefined;
  staffMember: StaffMember | undefined;

  @Output() submitSearch = new EventEmitter<string>();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public searchService: SearchService,
    public staffMemberService: StaffMemberService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params.id;
    this.searchService.query = id;
    this.loadStaffMember(id);
  }

  loadStaffMember(id: string) {
    this.errorMessage = undefined;
    this.isLoading = true;
    this.staffMemberService.get(id).subscribe(
      (result) => {
        this.staffMember = result;
        this.isLoading = false;
      },
      () => {
        this.isLoading = false;
        this.errorMessage =
          'Personalakte kann nicht geladen werden. Keine Verbindug zum Server. Bitte versuchen Sie es später wieder.';
      }
    );
  }

  search(query: string) {
    this.router.navigate(['search'], { queryParams: {
      q: query
    } });
    this.submitSearch.emit(query);
  }

}
