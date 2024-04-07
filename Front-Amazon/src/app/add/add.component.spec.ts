import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SomeactionsComponent } from './add.component';

describe('SomeactionsComponent', () => {
  let component: SomeactionsComponent;
  let fixture: ComponentFixture<SomeactionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SomeactionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SomeactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
