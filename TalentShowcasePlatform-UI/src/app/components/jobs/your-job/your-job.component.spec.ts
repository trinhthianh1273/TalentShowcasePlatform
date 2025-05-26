import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YourJobComponent } from './your-job.component';

describe('YourJobComponent', () => {
  let component: YourJobComponent;
  let fixture: ComponentFixture<YourJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YourJobComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YourJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
