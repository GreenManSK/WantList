import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimeStatsComponent } from './anime-stats.component';

describe('AnimeStatsComponent', () => {
  let component: AnimeStatsComponent;
  let fixture: ComponentFixture<AnimeStatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnimeStatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnimeStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
