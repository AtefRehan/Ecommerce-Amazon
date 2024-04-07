import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddsupplierComponent } from './addsupplier.component';

describe('AddsupplierComponent', () => {
  let component: AddsupplierComponent;
  let fixture: ComponentFixture<AddsupplierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddsupplierComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddsupplierComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
