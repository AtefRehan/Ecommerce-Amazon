import { Component, OnInit } from '@angular/core';
import { Product } from 'src/interfaces/product';
import { GlobalstateService } from 'src/service/globalstate.service';
import { Router } from '@angular/router';
import { ProductsService } from 'src/service/products.service';
import { CartService } from 'src/service/cart.service';

@Component({
  selector: 'app-showproduct',
  templateUrl: './showproduct.component.html',
  styleUrls: ['./showproduct.component.css']
})
export class ShowproductComponent implements OnInit {

  constructor(private globalState: GlobalstateService, private router: Router,private productservice:ProductsService,private cartService:CartService) { }
  items: Product[] | undefined
  searchedItem: string=''
  cart: Product[] =[]

  
  ngOnInit(): void {
    this.globalState.productsSubscription.subscribe(res => {
      this.items = res
    }, err => {
      console.log(err)
    })

    this.globalState.searchedItemSubscription.subscribe((res)=>{

      if(res!="")
      {

        this.items=this.globalState.getSearchedItemsResult(res)
        this.searchedItem=res
      }
      else{
        this.items=this.globalState.getProducts()
        this.searchedItem=''
      }

    },err => {
      console.log(err)
    })
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
