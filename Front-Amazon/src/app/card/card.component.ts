import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from 'src/interfaces/product';
import { CartService } from 'src/service/cart.service';
import { GlobalstateService } from 'src/service/globalstate.service';


@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent {

  constructor(private globalState:GlobalstateService,private cartService:CartService,private http:HttpClient){}
  productId: string = '';
  userID: string = '';
  @Input()item!:Product
  @Input() ind:number | undefined
  @Output() customEvent = new EventEmitter<Product>()
  isHeartClicked: boolean = false;


  selectItems():void{
    this.globalState.updateItemsInCart(this.item)
  }


  addToCart(productId: number): void {
    console.log("tahaaa");
    if (this.item && typeof this.item === 'object') {
      this.cartService.addToCart(this.item).subscribe(
        () => {
          console.log('Product added to cart successfully.');
        },
        error => {
          console.error('Error adding product to cart:', error);
        }
      );
    } else {
      console.error('Invalid item object:', this.item);
    }
  }


      addToWishlist(productId:number) {
        let userId = localStorage.getItem('userId');
        const url = `http://localhost:5189/api/WishList?productId=${productId}&userID=${userId}`;

        this.http.post(url, {}).subscribe(
          () => {
            alert('Product added to wishlist successfully!');
            this.productId = '';
            this.userID = '';
          },
          error => {
            console.error('Error adding product to wishlist:', error);
            alert('Error adding product to wishlist. Please try again later.');
          }
        );
      }


      HeartColor(productId: number): void {
          this.isHeartClicked = !this.isHeartClicked;
      }

}
