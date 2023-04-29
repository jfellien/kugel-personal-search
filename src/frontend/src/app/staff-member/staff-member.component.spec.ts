import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffMemberComponent } from './staff-member.component';

describe('PersonComponent', () => {
  let component: StaffMemberComponent;
  let fixture: ComponentFixture<StaffMemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaffMemberComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});