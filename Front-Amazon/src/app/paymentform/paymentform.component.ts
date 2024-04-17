import { Component, OnInit, ViewChild } from '@angular/core';
import { StripeCardNumberComponent, StripeService } from 'ngx-stripe';
import { PaymentIntent, StripeCardElementOptions, StripeElementsOptions } from '@stripe/stripe-js';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { switchMap } from 'rxjs';
import { CheckoutService } from 'src/service/checkout.service';
import { GlobalstateService } from 'src/service/globalstate.service';
import { Router } from '@angular/router';
import { Product } from 'src/interfaces/product';
import { UserDetails } from 'src/interfaces/userdetails';
import { HttpClient } from '@angular/common/http';
import { CartService } from 'src/service/cart.service';

@Component({
  selector: 'app-paymentform',
  templateUrl: './paymentform.component.html',
  styleUrls: ['./paymentform.component.css']
})
export class PaymentformComponent implements OnInit{

// implements OnInit {
//   isTokenExist=localStorage.getItem('token');

//   [x: string]: any
//   @ViewChild(StripeCardNumberComponent) card!: StripeCardNumberComponent;

//   constructor(private fb: FormBuilder, private stripeService: StripeService, private chechoutService: CheckoutService, private globalState: GlobalstateService, private router: Router) { }

//   paymentMessage: string | undefined | null = null

//   paymentHistory: boolean | undefined

//   currentUser: UserDetails | undefined

//   itemsInCart: Product[] = []

//   checkoutPrice:number=0

//   ngOnInit(): void {

//     this.globalState.itemsInCartSubscription.subscribe((res) => {
//       this.itemsInCart = res
//     })
//     this.globalState.currentUserSubscription.subscribe((res)=>{
//       console.log("currentUser-->",res)
//       this.currentUser=res
//     })

//     this.globalState.checkoutPriceSubscription.subscribe(res=>{
//       console.log("checkoutPrice-->",res)
//       this.checkoutPrice=res
//     })
//   }

//   public cardOptions: StripeCardElementOptions = {
//     style: {
//       base: {

//         iconColor: '#666EE8',
//         color: '#31325F',
//         fontWeight: '400',
//         fontSmoothing: 'antialiased',
//         fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
//         fontSize: '18px',
//         '::placeholder': {
//           color: '#CFD7E0',
//         },
//       },
//       invalid: {
//         color: '#fa755a',
//         iconColor: '#fa755a',
//       },
//       complete: {
//         color: '#4caf50',
//       },
//     },
//   };

//   public elementsOptions: StripeElementsOptions = {
//     locale: 'en',
//   };

//   paymentForm: FormGroup = this.fb.group({
//     line1: ['', Validators.required],
//     postal_code: ['', Validators.required],
//     city: ['Diarb Negm', Validators.required],
//     state: ['El Sharqia', Validators.required],
//     countryCode: ['EG', Validators.required],
//   });

//   pay() {
//     if (this.paymentForm && this.paymentForm && this.currentUser) {
//       const reqBody = { ...this.currentUser, amount:this.checkoutPrice, ...this.paymentForm.value, products: [...this.itemsInCart] }
//       console.log("reqBody,", reqBody)
//       this.chechoutService.createPaymentIntent(reqBody)
//         .pipe(
//           switchMap((pi: PaymentIntent) => {
//             console.log("outer pi.client_secret" + pi.client_secret)
//             if (pi && pi.client_secret) {
//               console.log("pi.client_secret" + pi.client_secret);
//               return this.stripeService.confirmCardPayment(pi.client_secret, {
//                 payment_method: {
//                   card: this.card?.element,
//                   billing_details: {
//                     name: this.paymentForm.get('name')?.value,
//                   },
//                 },
//               });
//             } else {
//               throw new Error('Invalid PaymentIntent or client_secret is null');
//             }
//           })
//         )
//         .subscribe((result) => {
//           if (result.error) {
//             console.log("error message");
//             console.log(result.error.message);
//             this.paymentHistory = true;
//             this.paymentMessage = result.error.message
//             setTimeout(() => {
//               this.paymentHistory = false;
//             }, 3000)
//           } else {


//             if (result.paymentIntent?.status === 'succeeded') {
//               this.globalState.emptyCart()
//               this.paymentHistory = true;
//               this.paymentMessage = "Thank you for the payment, Redirecting back to homepage"
//               setTimeout(() => {
//                 this.router.navigate([''])
//               }, 5000)
//             }
//           }
//         }, (error) => {
//           console.error('123 Error processing payment:', error);
//           this.paymentHistory = true;
//           this.paymentMessage = error.error.code
//           setTimeout(() => {
//             this.paymentHistory = false;
//           }, 5000)
//         });
//     }
cardNumber: string | undefined;
cardType: string | undefined;
expireDate: string | undefined;
paymentForm!: FormGroup;

constructor(private http: HttpClient, private cartService: CartService, private formBuilder: FormBuilder) { }

ngOnInit(): void {
  this.paymentForm = this.formBuilder.group({
    cardNumber: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]],
    cardType: ['', [Validators.required, Validators.pattern(/^(credit|debit)$/)]],
    expireDate: ['', [Validators.required]]
  });
}

submitPaymentForm(): void {
  if (this.paymentForm.invalid) {
    console.log('Form is invalid. Please fill in correct details.');
    return;
  }

  const cartId = localStorage.getItem('cartId') || 'default';

  this.cartService.getCartItems(+cartId).subscribe(
    (productsInCart) => {
      console.log('Products retrieved successfully:', productsInCart);
      const confirmation = window.confirm(`Are you sure you want to proceed with the payment?}`);
      if (!confirmation) {
        console.log('Payment cancelled.');
        return;
      }

      const newPayment = {
        card_Num: this.paymentForm.value.cardNumber,
        cardType: this.paymentForm.value.cardType,
        expireDate: this.paymentForm.value.expireDate,
        products: productsInCart
      };

      this.http.post<any>('http://localhost:5189/api/Payment', newPayment)
        .subscribe(
          (paymentData: any) => {
            console.log('Payment submitted successfully:', paymentData);
            const orderData = {
              cartId: cartId,
              paymentId: paymentData.paymentId
            };

            this.http.post('http://localhost:5189/api/Order/CreateOrder', orderData)
              .subscribe(
                (orderResponse) => {
                  console.log('Order created successfully:', orderResponse);
                  this.resetForm();
                  this.deleteCart(cartId);
                },
                (error) => {
                  console.error('Error creating order:', error);
                }
              );
          },
          (error) => {
            console.error('Error submitting payment:', error);
          }
        );
    },
    (error) => {
      console.error('Error retrieving products from the cart:', error);
    }
  );
}

deleteCart(cartId: string): void {
  const confirmation = window.confirm("If you continue, all products in your cart will be deleted.");
  if (!confirmation) {
    console.log('Deletion cancelled.');
    return;
  }

  this.http.delete(`http://localhost:5189/api/ProductInCart/Cart/${cartId}`)
    .subscribe(
      () => {
        console.log('Cart deleted successfully.');
      },
      (error) => {
        console.error('Error deleting cart:', error);
      }
    );
}

resetForm(): void {
  this.paymentForm.reset();
}
}
