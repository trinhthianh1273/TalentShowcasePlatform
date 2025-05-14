import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletingPopupComponent } from './completing-popup.component';

describe('CompletingPopupComponent', () => {
  let component: CompletingPopupComponent;
  let fixture: ComponentFixture<CompletingPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompletingPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompletingPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
