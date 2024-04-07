import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SignInComponent } from './signin/signin.component';
import { HomepageComponent } from './homepage/homepage.component';
import { CardComponent } from './card/card.component';
import { CartComponent } from './cart/cart.component';
import { BannerComponent } from './banner/banner.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaymentformComponent } from './paymentform/paymentform.component';
import { environment } from "../environments/environment";
import { NgxStripeModule } from 'ngx-stripe';
import { SignupComponent } from './signup/signup.component';
import { OrderDetailsComponent } from './orders/order-details.component';
import { ShowproductComponent } from './showproduct/showproduct.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';
import { UsersComponent } from './users/users.component';
import { SubcategoryComponent } from './subcategory/subcategory.component';
import { ProductComponent } from './product/product.component';
import { AddcategoryComponent } from './addcategory/addcategory.component';
import { AddsubcategoryComponent } from './addsubcategory/addsubcategory.component';
import { AddsupplierComponent } from './addsupplier/addsupplier.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ForgetpassComponent } from './forgetpass/forgetpass.component';
import { TestComponent } from './test/test.component';
import { TessssssstComponent } from './tessssssst/tessssssst.component';
import { NewpassComponent } from './newpass/newpass.component';
import { AboutusComponent } from './aboutus/aboutus.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SignInComponent,
    HomepageComponent,
    CardComponent,
    CartComponent,
    BannerComponent,
    PaymentformComponent,AboutusComponent,
    SignupComponent,ForgetpassComponent,TessssssstComponent,
    ShowproductComponent,DashboardComponent,NewpassComponent,
    ProductDetailsComponent,WishlistComponent,
    NotfoundComponent,OrderDetailsComponent,AddsupplierComponent,
   AddComponent,EditComponent, UsersComponent, SubcategoryComponent, ProductComponent, AddcategoryComponent, AddsubcategoryComponent, AddsupplierComponent, WishlistComponent, CartComponent, DashboardComponent, ForgetpassComponent, TestComponent, TessssssstComponent, NewpassComponent, AboutusComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    NgxStripeModule.forRoot(environment.stripe.publicKey),
    FormsModule,
  ],
  providers:[],
  bootstrap: [AppComponent]
})
export class AppModule { }
