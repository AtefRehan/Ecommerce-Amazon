import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/interfaces/product';
import { GlobalstateService } from 'src/service/globalstate.service';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  selectedItem: Product | null = null;
  itemsInCart: Product[] = []
  @Input()item!:Product
  product: any;

  constructor(private globalState: GlobalstateService, private route: ActivatedRoute) { }
  private itemsInCartSubject = new BehaviorSubject<Product[]>([]);
  itemsInCart$ = this.itemsInCartSubject.asObservable();
  ngOnInit(): void {
      this.product = history.state.product;
  }



  updateItemsInCart(item: Product) {
    let existingItems: Product[] = JSON.parse(localStorage.getItem('cartItems') || '[]');
    existingItems.push(item);
    localStorage.setItem('cartItems', JSON.stringify(existingItems));
    this.itemsInCartSubject.next(existingItems);
  }
  selectItems():void{
    this.globalState.updateItemsInCart(this.item)
  }
}
