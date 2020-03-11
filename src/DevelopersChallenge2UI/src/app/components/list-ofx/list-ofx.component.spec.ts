import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListOfxComponent } from './list-ofx.component';

describe('ListOfxComponent', () => {
  let component: ListOfxComponent;
  let fixture: ComponentFixture<ListOfxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListOfxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListOfxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
