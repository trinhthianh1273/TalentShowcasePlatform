import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileAwardsComponent } from './profile-awards.component';

describe('ProfileAwardsComponent', () => {
  let component: ProfileAwardsComponent;
  let fixture: ComponentFixture<ProfileAwardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileAwardsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileAwardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
