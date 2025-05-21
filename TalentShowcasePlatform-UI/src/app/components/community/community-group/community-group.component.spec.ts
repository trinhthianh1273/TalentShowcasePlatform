import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommunityGroupComponent } from './community-group.component';

describe('CommunityGroupComponent', () => {
  let component: CommunityGroupComponent;
  let fixture: ComponentFixture<CommunityGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommunityGroupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommunityGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
