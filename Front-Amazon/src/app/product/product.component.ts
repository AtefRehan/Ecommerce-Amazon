import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/interfaces/product';
import { ProductsService } from 'src/service/products.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  subcategoryId: number | null = null;
  products: any[] = [];
  items: Product[] | undefined
item!: Product;

  constructor(private route: ActivatedRoute,private router:Router, private http: HttpClient,private productservice:ProductsService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const subcategoryIdParam = params.get('subcategoryId');
      if (subcategoryIdParam !== null) {
        this.subcategoryId = +subcategoryIdParam;
        this.fetchProducts(this.subcategoryId);
      } else {
        console.error('Subcategory ID is null or undefined.');
      }
    });
  }

  fetchProducts(subcategoryId: number): void {
    const apiUrl = `http://localhost:5189/api/SubCategory/${subcategoryId}`;
    this.http.get<any>(apiUrl).subscribe(
      response => {
        this.products = response.products;
      },
      error => {
        console.error('Error fetching products:', error);
      }
    );
  }

  showProductDetails(itemId: number): void {
    this.productservice.getProductById(itemId).subscribe(
      (product) => {
        this.router.navigate(['/item-details', itemId], { state: { product } });
      },
      (error) => {
        console.error('Error fetching product details:', error);
      }
    );
  }
}
