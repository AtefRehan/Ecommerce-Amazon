import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './signin/signin.component';
import { CartComponent } from './cart/cart.component';
import { HomepageComponent } from './homepage/homepage.component';
import { PaymentformComponent } from './paymentform/paymentform.component';
import { SignupComponent } from './signup/signup.component';
import { OrderDetailsComponent } from './orders/order-details.component';
import { ShowproductComponent } from './showproduct/showproduct.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';
import { DeletComponent } from './delete/delete.component';
import { UsersComponent } from './users/users.component';
import { SubcategoryComponent } from './subcategory/subcategory.component';
import { ProductComponent } from './product/product.component';
import { AddcategoryComponent } from './addcategory/addcategory.component';
import { AddsubcategoryComponent } from './addsubcategory/addsubcategory.component';
import { AddsupplierComponent } from './addsupplier/addsupplier.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './auth.guard';
import { ForgetpassComponent } from './forgetpass/forgetpass.component';
import { TestComponent } from './test/test.component';
import { TessssssstComponent } from './tessssssst/tessssssst.component';
import { NewpassComponent } from './newpass/newpass.component';
import { AboutusComponent } from './aboutus/aboutus.component';

let routes: Routes = [
    {path:'',component:HomepageComponent,title:"Amazon Home"},
    {path:'cart',component:CartComponent,title:"Amazon Cart"},
    {path:'test',component:TestComponent,title:"Amazon Cart"},
    {path:'aboutus',component:AboutusComponent,title:"Amazon About us"},
    {path:'asd',component:TessssssstComponent,title:"Amazon Cart"},
    {path:'signin',component:SignInComponent,title:"Amazon SignIn"},
    {path:'signup',component:SignupComponent,title:"Amazon SignUp"},
    {path:'forgetpass',component:ForgetpassComponent,title:"Amazon forgetpass"},
    {path:'edit',component:EditComponent, title:"Amazon edit"},
    {path:'addsupplier',component:AddsupplierComponent, title:"Amazon addsupplier"},
    {path:'add',component:AddComponent,title:"Amazon add"},
    {path:'dashboard',component:DashboardComponent, title:"Amazon dashboard"},
    {path:'wishlist',component:WishlistComponent, title:"Amazon wishlist"},
    {path:'newpass',component:NewpassComponent, title:"Amazon New Password"},
    {path:'delete',component:DeletComponent,  title:"Amazon delete"},
    {path:'checkout',component:PaymentformComponent,title:"Amazon Checkout Payment"},
    {path:'orders',component:OrderDetailsComponent,title:"Amazon Order"},
    {path:'addsubcategory',component:AddsubcategoryComponent,title:"Amazon addsubcategory"},
    {path:'addcategory',component:AddcategoryComponent,title:"Amazon addcategory"},
    {path:'show product',component:ShowproductComponent,title:"Amazon Show Product"},
    { path: 'products/:subcategoryId', component: ProductComponent },
    {path:'users',component:UsersComponent,title:"Amazon Users"},
    { path: 'subcategories/:categoryId', component: SubcategoryComponent },
    { path: 'item-details/:productId', component: ProductDetailsComponent ,title:"Amazon item-details"},
     {path:'**',component:NotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
