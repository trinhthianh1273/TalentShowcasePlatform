import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserVideoComponent } from './user-video.component';

describe('UserVideoComponent', () => {
  let component: UserVideoComponent;
  let fixture: ComponentFixture<UserVideoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserVideoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
