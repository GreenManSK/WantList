import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaTitleComponent } from './manga-title.component';

describe('MangaTitleComponent', () => {
  let component: MangaTitleComponent;
  let fixture: ComponentFixture<MangaTitleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaTitleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
