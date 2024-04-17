import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5189/api/ProductInCart';

  constructor(private http: HttpClient) { }


  addToCart(item: any): Observable<any> {
    let cartId = localStorage.getItem('cartId') || 'default';
    let productId = item.productId;
    item.stock--;
    return this.http.get<any[]>(`http://localhost:5189/api/ProductInCart/${cartId}`).pipe(
      switchMap((cartItems: any[]) => {
        const existingItem = cartItems.find(item => item.product.productId === productId);
        if (existingItem) {
          const newQuantity = existingItem.quantity + 1;
          return this.http.post<any>(`${this.apiUrl}/${existingItem.id}/increasequantity`, { quantity: newQuantity }).pipe(
            catchError(error => {
              console.error('Error increasing product quantity in cart:', error);
              throw error;
            })
          );
        } else {
          const body = { productId, cartId, quantity: 1 };
          return this.http.post<any>(this.apiUrl, body).pipe(
            catchError(error => {
              console.error('Error adding product to cart:', error);
              throw error;
            })
          );
        }
      }),
      catchError(error => {
        console.error('Error fetching cart items:', error);
        throw error;
      })
    );
  }


  getCartItems(cartId?: number): Observable<any[]> {
    let url = this.apiUrl;
    if (cartId) {
      url += `/${cartId}`;
    }
    return this.http.get<any[]>(url);
  }


  decreaseQuantityByCartId(productId: number): Observable<any> {
    let cartId = localStorage.getItem('cartId');
    if (!cartId) {
      console.error('Cart ID not found in local storage.');
      return throwError('Cart ID not found in local storage.');
    }
    return this.http.get<any[]>(`${this.apiUrl}/${cartId}`).pipe(
      switchMap((cartItems: any[]) => {
        let cartItem = cartItems.find(item => item.product.productId === productId);
        if (!cartItem) {
          console.error('Error: Cart item not found');
          return throwError('Cart item not found');
        }
        let idInDB = cartItem.id;
        let url = `${this.apiUrl}/${idInDB}/decreasequantity`;
        return this.http.post(url, {}).pipe(
          switchMap(() => {
            if (cartItem.quantity === 1) {
              return this.http.delete(`${this.apiUrl}/${idInDB}`).pipe(
                switchMap(() => {
                  console.log('Product removed from cart');
                  return this.http.get<any[]>(`${this.apiUrl}/${cartId}`);
                }),
                catchError(error => {
                  console.error('Error retrieving updated cart items:', error);
                  throw error;
                })
              );
            } else {
              return of(false);
            }
          }),
          catchError(error => {
            console.error('Error decreasing product quantity:', error);
            throw error;
          })
        );
      }),
      catchError(error => {
        console.error('Error retrieving product in cart:', error);
        throw error;
      })
    );}


  increaseQuantityByCartId(productId: number): Observable<any> {
    let cartId = localStorage.getItem('cartId');
    if (!cartId) {
      console.error('Cart ID not found in local storage.');
      return throwError('Cart ID not found in local storage.');
    }
    return this.http.get<any[]>(`${this.apiUrl}/${cartId}`).pipe(
      switchMap((cartItems: any[]) => {
        const cartItem = cartItems.find(item => item.product.productId === productId);
        if (!cartItem) {
          console.error('Error: Cart item not found');
          return throwError('Cart item not found');
        }

        let idInDB = cartItem.id;
        let url = `${this.apiUrl}/${idInDB}/increasequantity`;
        return this.http.post(url, {}).pipe(
          catchError(error => {
            console.error('Error increasing product quantity:', error);
            throw error;
          })
        );
      }),
      catchError(error => {
        console.error('Error retrieving product in cart:', error);
        throw error;
      })
    );
  }



  deleteProductFromCart(productId: number): Observable<any> {
    const url = `${this.apiUrl}/product/${productId}`;
    return this.http.delete(url).pipe(
      catchError(error => {
        console.error('Error deleting product from cart:', error);
        throw error;
      })
    );
  }




getProduct(): Observable<any> {
    return this.http.get<any[]>(this.apiUrl).pipe(
      map(products => {
        if (products && products.length > 0) {
          return products[0].productsInCart[0].product;
        } else {
          throw new Error('No product found in the response.');
        }
      }),
      catchError(error => {
        console.error('Error getting product:', error);
        throw error;
      })
    );
  }

}
