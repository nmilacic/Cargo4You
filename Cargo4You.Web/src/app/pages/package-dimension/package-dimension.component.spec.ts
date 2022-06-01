import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageDimensionComponent } from './package-dimension.component';

describe('PackageDimensionComponent', () => {
  let component: PackageDimensionComponent;
  let fixture: ComponentFixture<PackageDimensionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PackageDimensionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PackageDimensionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
