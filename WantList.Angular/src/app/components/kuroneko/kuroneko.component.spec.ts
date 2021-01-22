import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KuronekoComponent } from './kuroneko.component';

describe('KuronekoComponent', () => {
  let component: KuronekoComponent;
  let fixture: ComponentFixture<KuronekoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KuronekoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KuronekoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
