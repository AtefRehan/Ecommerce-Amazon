import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Product } from 'src/interfaces/product';
import { ProductsService } from 'src/service/products.service';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent {

  productId!: string;
  productForm!: FormGroup;
  product!: Product;

  constructor(
    private formBuilder: FormBuilder,
    private httpProductsService: ProductsService,
    private http: HttpClient
  ) {}

  onSubmit(): void {
    if (this.productForm.invalid) {
      return;
    }

    let formData = this.productForm.value;
    let productId = this.productForm.get('productId')?.value;

    if (!productId) {
      console.error('Product ID is missing. Aborting product update.');
      return;
    }

    this.httpProductsService.updateProduct(productId, formData).subscribe(
      (response) => {
        console.log('Product updated successfully:', response);
        this.productForm.reset();
      },
      (error) => {
        console.error('Error updating product:', error);
      }
    );
  }

  onProductIdChange(event: any): void {
    this.productId = event.target.value;
    console.log('Product ID changed:', this.productId);
  }

  // onSubmit(): void {
  //   if (this.productForm.invalid) {
  //     return;
  //   }

  //   let formData = this.productForm.value;

  //   if (!this.productId) {
  //     console.error('Product ID is missing. Aborting product update.');
  //     return;
  //   }

  //   this.httpProductsService.updateProduct(this.productId, formData).subscribe(
  //     (response) => {
  //       console.log('Product updated successfully:', response);
  //       this.productForm.reset();
  //     },
  //     (error) => {
  //       console.error('Error updating product:', error);
  //     }
  //   );
  // }




  getProductById(): void {
    if (!this.productId) {
      console.log('Invalid Product ID:', this.productId);
      console.error('Product ID is required');
      return;
    }
    let productIdNumber = Number(this.productId);

    console.log('Fetching product details for ID:', productIdNumber);
    this.httpProductsService.getProductById(productIdNumber).subscribe(
      (response) => {
        this.product = response;
        console.log('Product details fetched successfully:', this.product);
        this.productForm.patchValue({
          name: this.product.name,
          price: this.product.price,
          stock: this.product.stock,
          weight: this.product.weight,

          description: this.product.description,

          category: this.product.category,
          image: this.product.image,
          supplierId: this.product.supplierId,
          subCategoryId: this.product.subCategoryId
        });
      },
      (error) => {
        alert("This product is unavailable !");
        console.error('Error fetching product details:', error);
      }
    );
  }


  ngOnInit(): void {
    this.productForm = this.formBuilder.group({
      productId: [''],
      name: [''],
      price: [''],
      stock: [''],
      weight: [''],
      description: [''],
      category: [''],
      image: [''],
      supplierId: [''],
      subCategoryId: ['']
    });
  }


  updateProduct(productId: string, product: Product): Observable<Product> {
    if (!productId) {
      console.error('Invalid Product ID:', productId);
      return throwError('Product ID is required');
    }

    return this.http.put<Product>(`http://localhost:5281/api/Products/${productId}`, product).pipe(
      tap((response) => {
        console.log('Product updated successfully:', response);
      }),
      catchError((error) => {
        console.error('Error updating product:', error);
        return throwError('Error updating product');
      })
    );
  }
}
