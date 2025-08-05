import { Component, effect, inject, output } from '@angular/core';
import { CheckoutService } from '../../../Core/Services/checkout.service';
import {MatRadioButton, MatRadioGroup} from '@angular/material/radio';
import { CurrencyPipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { CartService } from '../../../Core/Services/cart-service';
import { DeliveryMethod } from '../../../Shared/models/deliveryMethod';
import { Cart } from '../../cart/cart';

@Component({
  selector: 'app-checkout-delivery',
  imports: [MatRadioGroup,
     MatRadioButton,
     CurrencyPipe,
    ],
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss'
})
export class CheckoutDeliveryComponent  {
  checkoutService=inject(CheckoutService)
  deliveryMethods=toSignal(this.checkoutService.getDeliveryMethods(),{initialValue:[]})
  cartService=inject(CartService)
  deliveryComplete=output<boolean>()
 
  private deliveryMethodEffect= effect(()=>{
  const deliveryMethods= this.deliveryMethods()
    if(this.cartService.cart()?.deliveryMethodId){
        const method=deliveryMethods.find(x=>x.id===this.cartService.cart()?.deliveryMethodId)
        if(method){
          this.cartService.selectedDelivery.set(method)
          this.deliveryComplete.emit(true)
         }
     }
 });
    
  updateDeliveryMethod(method:DeliveryMethod){
    this.cartService.selectedDelivery.set(method)
    const cart=this.cartService.cart()
    if(cart){
      cart.deliveryMethodId=method.id
      this.cartService.setCart(cart)
      this.deliveryComplete.emit(true)
    }
  }

}
