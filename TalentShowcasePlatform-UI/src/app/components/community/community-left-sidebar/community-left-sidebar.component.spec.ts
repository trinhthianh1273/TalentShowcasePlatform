import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommunityLeftSidebarComponent } from './community-left-sidebar.component';

describe('CommunityLeftSidebarComponent', () => {
  let component: CommunityLeftSidebarComponent;
  let fixture: ComponentFixture<CommunityLeftSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommunityLeftSidebarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommunityLeftSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
