import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CartService } from 'src/service/cart.service';
import { Product } from 'src/interfaces/product';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  productId: string = '';
  userID: string = '';
  products: any[] = [];
  selectedItems: Product[] = [];
  isTokenExist = localStorage.getItem('LoginExist!');
  @Input()item!:Product

  constructor(private http: HttpClient,private cartService:CartService) { }


    ngOnInit(): void {
      this.getWishlist();
    }

    getWishlist() {
      let userId = localStorage.getItem('userId');
      const url = `http://localhost:5189/api/WishList?userId=${userId}`;

      this.http.get<any[]>(url).subscribe(
        (data: any[]) => {
          this.products = data;
        },
        error => {
          console.error('Error fetching wishlist:', error);
        }
      );
    }

    delete(productId:number) {
      let userId = localStorage.getItem('userId');
      const url = `http://localhost:5189/api/WishList?productId=${productId}&userID=${userId}`;

      this.http.delete<any[]>(url).subscribe(
        (data: any[]) => {
          this.products = data;
          this.getWishlist()
        },
        error => {
          console.error('Error fetching wishlist:', error);
        }
      );
    }

    addToCart(productId: number): void {
      console.log("Adding product to cart. ProductId:", productId);
      let productToAdd = { productId: productId };
      this.cartService.addToCart(productToAdd).subscribe(
        (response) => {
          console.log('Product added to cart successfully.', response);
        },
        error => {
          console.error('Error adding product to cart:', error);
        }
      );
    }


  }
