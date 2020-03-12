import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadOfxComponent } from './upload-ofx.component';

describe('UploadOfxComponent', () => {
  let component: UploadOfxComponent;
  let fixture: ComponentFixture<UploadOfxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadOfxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadOfxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
