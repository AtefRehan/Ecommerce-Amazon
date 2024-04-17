import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from 'src/interfaces/product';
import { UserDetails } from 'src/interfaces/userdetails';
import { CartService } from 'src/service/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  isTokenExist = localStorage.getItem('LoginExist!');
  itemsInCart: Product[] = [];
  totalCheckoutCost: number = 0;
  currentUser: UserDetails | undefined;
  productId!: number;
  cartId: number = 0;
  product: any;
  id!: number;

  constructor( private cartService: CartService,private http:HttpClient) { }

    ngOnInit(): void {
      let productId = localStorage.getItem('cartId');
      if (productId) {
        this.getCartItemsByProductId(+productId);
      } else {
        console.error('Product ID not found in local storage.');
      }
      }

  getCartItemsByProductId(productId: number): void {
    this.cartService.getCartItems(productId).subscribe(
      (items) => {
        console.log('Cart items:', items);
        this.itemsInCart = items;
        console.log('Cart items:', this.itemsInCart);
        console.log(this.itemsInCart[0].product.image);
        this.calculateTotalCost();
      },
      (error) => {
        console.error('Error fetching cart items:', error);
      }
    );
  }


  calculateTotalCost(): number {
    let totalCost = 0;
    for (const item of this.itemsInCart) {
      totalCost += item.product.price * item.quantity;
    }
    return totalCost;
  }
  getTotalQuantity(): number {
    let totalQuantity = 0;
    for (const item of this.itemsInCart) {
      totalQuantity += item.quantity;
    }
    return totalQuantity;
  }

  removeFromCart(productId: number): void {
    this.cartService.deleteProductFromCart(productId).subscribe(
      () => {
        console.log('Product deleted from cart successfully.');
        const index = this.itemsInCart.findIndex(item => item.product.productId === productId);
        if (index !== -1) {
          this.itemsInCart.splice(index, 1);
        }
      },
      error => {
        console.error('Error deleting product from cart:', error);
      }
    );
  }



  increaseQuantity(productId: number): void {
    let cartId = localStorage.getItem('cartId');
    if (!cartId) {
      console.error('Cart ID not found in local storage.');
      return;
    }

    this.cartService.increaseQuantityByCartId(productId).subscribe(
      () => {
        console.log('Quantity increased successfully');
        this.cartService.getCartItems(+cartId!).subscribe(
          (updatedCartItems) => {
            this.itemsInCart = updatedCartItems;
            this.calculateTotalCost();
          },
          error => {
            console.error('Error fetching updated cart items:', error);
          }
        );
      },
      error => {
        console.error('Error increasing quantity:', error);
      }
    );
  }



  decreaseQuantity(productId: number): void {
    let cartId = localStorage.getItem('cartId');
    if (!cartId) {
      console.error('Cart ID not found in local storage.');
      return;
    }

    this.cartService.decreaseQuantityByCartId(productId).subscribe(
      () => {
        console.log('Quantity decreased successfully');
        if (this.itemsInCart.find(item => item.product.productId === productId)?.quantity === 1) {
          this.removeFromCart(productId);
        } else {
          this.cartService.getCartItems(+cartId!).subscribe(
            (updatedCartItems) => {
              this.itemsInCart = updatedCartItems;
              this.calculateTotalCost();
            },
            error => {
              console.error('Error fetching updated cart items:', error);
            }
          );
        }
      },
      error => {
        console.error('Error decreasing quantity:', error);
      }
    );
  }



  // loadCartItems(): void {
  //   this.cartService.getCartItems().subscribe(
  //     (items) => {
  //       this.itemsInCart = items;
  //     },
  //     (error) => {
  //       console.error('Error retrieving cart items:', error);
  //     }
  //   );
  // }

  // decreaseQuantity(productId: number): void {
  //   this.cartService.decreaseQuantityByCartId(productId).subscribe(
  //     () => {
  //       console.log('Quantity decreased successfully');
  //       this.cartService.getCartItems();
  //     },
  //     error => {
  //       console.error('Error decreasing quantity:', error);
  //     }
  //   );
  // }
}



