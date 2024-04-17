import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsService } from 'src/service/products.service';
import { Product } from 'src/interfaces/product';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  productId: number | undefined;
  productForm!: FormGroup;
  productbyId: number = 0;
  product: Product | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private httpProductsService: ProductsService
  ) {}

  ngOnInit(): void {
    this.productForm = this.formBuilder.group({
      name: ['', Validators.required],
      price: ['', Validators.required],
      stock: ['', Validators.required],
      weight: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      image: ['', Validators.required],
      supplierId: ['', Validators.required],
      subCategoryId: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.productForm.invalid) {
      return;
    }
    const productData = this.productForm.value;
    this.httpProductsService.createProduct(productData).subscribe(
      (response) => {
        alert("product created successfuly")
        console.log('Product created successfully:', response);
      },
      (error) => {
        console.error('Error creating product:', error);
      }
    );
  }
}
