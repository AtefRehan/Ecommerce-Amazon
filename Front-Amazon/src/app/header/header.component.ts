import { Component, OnInit } from '@angular/core';
import { UserDetails } from 'src/interfaces/userdetails';
import { GlobalstateService } from 'src/service/globalstate.service';
import { ReversegeoencodingService } from 'src/service/reversegeoencoding.service';
import { Router } from '@angular/router';
import { Product } from 'src/interfaces/product';
import { CartService } from 'src/service/cart.service';
import { Observable, map, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoginExist = localStorage.getItem('LoginExist!');
  Nameuser = localStorage.getItem('NameUser!');
  headerSwitch:boolean=true;
  searchItem: string = '';
  itemImages = ['../../assets/box6_image.jpg', 'assets/item2.jpg', 'assets/item3.jpg','assets/item4.jpg', 'assets/item5.jpg', 'assets/item6.jpg', 'assets/item7.jpg', 'assets/item3.jpg','assets/item4.jpg', 'assets/item5.jpg', 'assets/item6.jpg', 'assets/item7.jpg', 'assets/item3.jpg','assets/item4.jpg', 'assets/item5.jpg', 'assets/item6.jpg', 'assets/item7.jpg',]
  totalQuantity!: number;

  itemsInCart: Product[] = []
  numberOfItemsInCart: number = 0
  currentUser!: UserDetails
  currentCity: string | undefined
  currentCountry: string | undefined
  postcode: string | undefined
  countryCode:string|undefined
  isAdmin: string | null = localStorage.getItem('IsAdmin!');
  filteredProducts: Product[] = [];
  searchQuery: string = '';
  products: Product[] = [];

  constructor(private http:HttpClient,private globalState: GlobalstateService, private currentLocation: ReversegeoencodingService,private router: Router,private cartService:CartService) { }

  ngOnInit(): void {
    this.globalState.currentUserSubscription.subscribe(res => {
      this.currentUser = res
    });
    this.globalState.headerSwitchSubscription.subscribe(res => {
      this.headerSwitch = res
    });
    this.currentLocation.getCurrentLocation().subscribe((cordinates: GeolocationCoordinates) => {
      this.currentLocation.getReverseLocation(cordinates.latitude, cordinates.longitude).subscribe((res) => {
        this.currentCity = res.city
        this.currentCountry = res.countryName
        this.postcode = res.postcode
        this.countryCode = res.countryCode
      });
    });
    this.globalState.itemsInCartSubscription.subscribe((res) => {
      this.itemsInCart = res;
    })
    let storedItemsInCart = localStorage.getItem('cartItems');
    if (storedItemsInCart) {
      this.itemsInCart = JSON.parse(storedItemsInCart);
    }
    const productId = localStorage.getItem('cartId');
    if (productId) {
      this.getCartItemsByProductId(+productId);
    } else {
      console.error('Product ID not found in local storage.');
    }
    if (productId) {
      this.getCartItemsByProductId(+productId);
      this.totalQuantity = this.getTotalQuantity();
    } else {
      console.error('Product ID not found in local storage.');
    }
  }

  search(): void {
    if (this.searchQuery.trim() !== '') {
      this.filteredProducts = this.products.filter(product =>
        product.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredProducts = [...this.products];
    }
  }

  getCartItemsByProductId(productId: number): void {
    this.cartService.getCartItems(productId).subscribe(
      (items) => {
        console.log('Cart items:', items);
        this.itemsInCart = items;
        console.log('Cart items:', this.itemsInCart);
        console.log(this.itemsInCart[0].product.image);
      },
      (error) => {
        console.error('Error fetching cart items:', error);
      }
    );
  }

  getTotalQuantity(): number {
    let totalQuantity = 0;
    for (const item of this.itemsInCart) {
      totalQuantity += item.quantity;
    }
    return totalQuantity;
  }

  // getQuantity(): Observable<number> {
  //   let cartId = localStorage.getItem('cartId');
  //   if (!cartId) {
  //     return throwError('Cart ID not found in local storage.');
  //   }

  //   return this.http.get<any[]>(`http://localhost:5189/api/ProductInCart/CartProducts/${cartId}`).pipe(
  //     map(cartProducts => {
  //       let totalQuantity = 0;
  //       for (const item of cartProducts) {
  //         totalQuantity += item.quantity;
  //       }
  //       return totalQuantity;
  //     })
  //   );
  // }
  // updateTotalQuantity() {
  //   this.totalQuantity = this.getTotalQuantity();
  // }

  // search() {
  //   this.globalState.getSearchedItemsResult(this.searchItem);
  // }

  searchByInput(event: Event) {
    let inputElement = event.target as HTMLInputElement;
    this.searchItem = inputElement.value;
    this.search();
  }

  signOut(): void {
    localStorage.removeItem('NameUser!');
    localStorage.removeItem('LoginExist!');
    localStorage.removeItem('cartItems');
    localStorage.removeItem('cartId');
    localStorage.removeItem('userId');
    localStorage.removeItem('IsAdmin!');
    this.router.navigate(['/signin']);
    window.location.reload();
  }
}
