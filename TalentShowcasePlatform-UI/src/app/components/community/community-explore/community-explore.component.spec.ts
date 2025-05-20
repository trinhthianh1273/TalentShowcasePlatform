import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommunityExploreComponent } from './community-explore.component';

describe('CommunityExploreComponent', () => {
  let component: CommunityExploreComponent;
  let fixture: ComponentFixture<CommunityExploreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommunityExploreComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommunityExploreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
