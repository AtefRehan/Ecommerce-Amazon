import { Component } from '@angular/core';
import { AuthService } from 'src/service/auth.service';
import { GlobalstateService } from 'src/service/globalstate.service';
import { ProductsService } from 'src/service/products.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Amazon';
  constructor(private global: GlobalstateService, private productservice: ProductsService, private auth: AuthService,private router:Router) {
    productservice.getProductData().subscribe((res) => {
      this.global.updateProducts(res)
    }, (e) => {
      console.log(e)
    })

    if(localStorage.getItem('token')) {this.auth.getCurrentUser().subscribe((res) => {this.global.updateCurrentUser(res)})}
  }

  showHeader(): boolean {
    return !this.router.url.includes('signin') && !this.router.url.includes('signup');
  }

  showFooter(): boolean {
    return !this.router.url.includes('signin') && !this.router.url.includes('signup');
  }
}

