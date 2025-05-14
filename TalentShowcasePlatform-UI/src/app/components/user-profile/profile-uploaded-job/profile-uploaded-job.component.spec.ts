import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileUploadedJobComponent } from './profile-uploaded-job.component';

describe('ProfileUploadedJobComponent', () => {
  let component: ProfileUploadedJobComponent;
  let fixture: ComponentFixture<ProfileUploadedJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileUploadedJobComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileUploadedJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
