import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { OrderSummaryComponent } from "../../Shared/components/order-summary/order-summary.component";
import { StripeService } from '../../Core/Services/stripe.service';
import {  ConfirmationToken, StripeAddressElement, StripeAddressElementChangeEvent, StripePaymentElement, StripePaymentElementChangeEvent } from '@stripe/stripe-js';
import { CheckoutDeliveryComponent } from "./checkout-delivery/checkout-delivery.component";
import {MatCheckboxChange, MatCheckboxModule} from '@angular/material/checkbox';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Address } from '../../Shared/models/User';
import { AccountService } from '../../Core/Services/account.service';
import { firstValueFrom } from 'rxjs';
import { CheckoutReviewComponent } from "./checkout-review/checkout-review.component";
import { MatButtonModule } from '@angular/material/button';
import { CartService } from '../../Core/Services/cart-service';
import { CurrencyPipe, JsonPipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
@Component({
  selector: 'app-checkout',
  imports: [MatStepperModule,
    OrderSummaryComponent,
    CheckoutDeliveryComponent,
    MatCheckboxModule,
    CheckoutReviewComponent,
    MatButtonModule,
    CurrencyPipe,
    JsonPipe,
    RouterLink,
    MatSnackBarModule
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit ,OnDestroy {
  
  private stripeService=inject(StripeService)
  private accountService=inject(AccountService)
  cartService=inject(CartService)
  router=inject(Router)
  snack=inject(MatSnackBar)
  addressElement?:StripeAddressElement
  paymentElement?:StripePaymentElement
  confirmationToken?:ConfirmationToken
  saveAddress=false
  completionStatus=signal<{address:boolean,card:boolean,delivery:boolean}>
  ({address:false,card:false,delivery:false})
  async ngOnInit() {
    try{
      this.addressElement= await this.stripeService.createAddressElements()
      
      this.addressElement.mount('#address-element')

      this.addressElement.on('change',this.handleAddressChange) //listens to change in stripe's address fields and call the handler method passed on change
      
      this.paymentElement=await this.stripeService.createPaymentElement();
      
      this.paymentElement.mount('#payment-element')

      this.paymentElement?.on('change',this.handlePaymentChange) //listens to change in stripe's payment fields and call the handler method passed on change
    
    }catch(error){
      console.log(error)
    }
  }

  handleAddressChange=(event:StripeAddressElementChangeEvent)=>{
    this.completionStatus.update(state=>{
      state.address=event.complete
      return state
    })
  }

  handlePaymentChange=(event:StripePaymentElementChangeEvent)=>{
    this.completionStatus.update(state=>{
      state.card=event.complete
      return state
    })
  }

  handleDeliveryChange=(event:boolean)=>{
    this.completionStatus.update(state=>{
      state.delivery=event
      return state
    })
  }



  ngOnDestroy(): void {
    this.stripeService.disposeElemnets()
  }

  async getConfirmationToken(){
    try{
      if(Object.values(this.completionStatus()).every(x=>x===true)){
        const result = await this.stripeService.createConfirmationToken()
        if(result.error) throw new Error(result.error.message)
          this.confirmationToken=result.confirmationToken
        console.log(this.confirmationToken)
      }
    }catch(error:any){
      console.log(error)
    }
  }

  async confirmPayment(stepper:MatStepper){
    try{
     const result= await this.stripeService.confirmPayment(this.confirmationToken)
      if(result?.error){
        throw new Error(result.error.message)
      }
      this.cartService.deleteCart()
      this.cartService.selectedDelivery.set(null)
      this.router.navigateByUrl('/checkout/success')
    }catch(error:any){
      this.snack.open(error.message||'something went wrong')
    }
  }

  async onStepChange(event:StepperSelectionEvent){
    if(event.selectedIndex===1){
      if(this.saveAddress){
        const address=await this.getAddressFromStripeAddress()
        console.log(address)
        address&& await  firstValueFrom((this.accountService.updateAddress(address)))
      }
    }
    if(event.selectedIndex===2){
      await firstValueFrom(this.stripeService.createOrUpdatePaymentIntent())
    }

    if(event.selectedIndex===3){
      await this.getConfirmationToken()
    }
  }
  private async getAddressFromStripeAddress():Promise<Address|null>{
    const result= await this.addressElement?.getValue()
    const address=result?.value.address
    console.log(address)
    if(address){  
      return {
        line1:address.line1,
        line2:address.line2||undefined,
        city:address.city,
        country:address.country,
        state:address.state,
        postalCode:address.postal_code
      } 
    }
    return null
  }

  onSaveAddressCheckBoxChange(event:MatCheckboxChange){
    this.saveAddress=event.checked
  }
}
