import { Injectable } from '@angular/core';
import { BehaviorSubject} from 'rxjs';
import { UserDetails } from 'src/interfaces/userdetails';
import { Product } from 'src/interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class GlobalstateService {

  constructor() { }

  products = new BehaviorSubject<Product[]>([])
  checkoutPrice = new BehaviorSubject<number>(0)
  itemsInCart = new BehaviorSubject<Product[]>([])
  currentUser = new BehaviorSubject<UserDetails>({
    username: null,email: null,
    phoneNumber: null,password:null,})


  selectedItem = new BehaviorSubject<Product>(null!)
  searchedItem = new BehaviorSubject<string>(null!)
  headerSwitch=new BehaviorSubject<boolean>(true)

  currentUserSubscription = this.currentUser.asObservable()
  itemsInCartSubscription = this.itemsInCart.asObservable()
  productsSubscription = this.products.asObservable()
  selectedItemSubscription = this.selectedItem.asObservable()
  searchedItemSubscription = this.searchedItem.asObservable()
  checkoutPriceSubscription=this.checkoutPrice.asObservable()
  headerSwitchSubscription=this.headerSwitch.asObservable()

  updateItemsInCart(product: Product): void {
    let items: Product[] = JSON.parse(localStorage.getItem('cartItems') || '[]') || [];
    items.push(product);
    localStorage.setItem('cartItems', JSON.stringify(items));
    product.stock--;
    this.itemsInCart.next(items);
    let totalPrice = items.reduce((acc, item) => acc + item.price, 0);
    this.checkoutPrice.next(totalPrice);
  }


  removeItemsInCart(index: number) {
    let items = JSON.parse(localStorage.getItem('cartItems') || '[]') as Product[];
    items.splice(index, 1);
    localStorage.setItem('cartItems', JSON.stringify(items));
    this.itemsInCart.next(items);
    let totalPrice = items.reduce((acc, item) => acc + item.price, 0);
    this.checkoutPrice.next(totalPrice);
  }

    updateHeaderSwitch(value:boolean){
      this.headerSwitch.next(value)
    }

  emptyCart() {
    this.itemsInCart.next([])
  }

  updateCurrentUser(user: UserDetails) {
    this.currentUser.next(user)
  }

  updateProducts(product: Product[]) {
    this.products.next(product)
  }

  updateSelectedItem(item: Product) {
    this.selectedItem.next(item)
  }



  updateSearchedItem(p: string) {
    this.searchedItem.next(p);
  }


  getProducts(): Product[] {
    return this.products.getValue();
  }



  getSelectedItem(): Product {
    return this.selectedItem.getValue()
  }


  getSearchededItem(): string {
    return this.searchedItem.getValue();
  }



  getSearchedItemsResult(searchedItem: string): Product[] {
    const res: Product[] = []
    this.products.getValue().filter((p: Product) => {
      if ((p.name.toLowerCase().includes(searchedItem.toLowerCase())
       || p.description.toLowerCase().includes(searchedItem.toLowerCase()))
       || p.category.toLowerCase().includes(searchedItem.toLowerCase())) {
        res.push(p);
      }
    })
    return res;
  }




 getItemsInCart():Product[]{
    return this.itemsInCart.getValue();
  }
}

