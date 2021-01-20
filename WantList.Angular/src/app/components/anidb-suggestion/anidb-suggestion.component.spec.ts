import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnidbSuggestionComponent } from './anidb-suggestion.component';

describe('AnidbSuggestionComponent', () => {
  let component: AnidbSuggestionComponent;
  let fixture: ComponentFixture<AnidbSuggestionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnidbSuggestionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnidbSuggestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
