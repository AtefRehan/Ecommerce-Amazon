import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TessssssstComponent } from './tessssssst.component';

describe('TessssssstComponent', () => {
  let component: TessssssstComponent;
  let fixture: ComponentFixture<TessssssstComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TessssssstComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TessssssstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
