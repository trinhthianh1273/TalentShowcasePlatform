import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileAchivementsComponent } from './profile-achivements.component';

describe('ProfileAchivementsComponent', () => {
  let component: ProfileAchivementsComponent;
  let fixture: ComponentFixture<ProfileAchivementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileAchivementsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileAchivementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
