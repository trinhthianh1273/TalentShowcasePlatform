import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommunityPopularComponent } from './community-popular.component';

describe('CommunityPopularComponent', () => {
  let component: CommunityPopularComponent;
  let fixture: ComponentFixture<CommunityPopularComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommunityPopularComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommunityPopularComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
