import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommunityAllComponent } from './community-all.component';

describe('CommunityAllComponent', () => {
  let component: CommunityAllComponent;
  let fixture: ComponentFixture<CommunityAllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommunityAllComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommunityAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
