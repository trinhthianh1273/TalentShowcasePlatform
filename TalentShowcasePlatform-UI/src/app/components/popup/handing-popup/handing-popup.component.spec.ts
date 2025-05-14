import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HandingPopupComponent } from './handing-popup.component';

describe('HandingPopupComponent', () => {
  let component: HandingPopupComponent;
  let fixture: ComponentFixture<HandingPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HandingPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HandingPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
