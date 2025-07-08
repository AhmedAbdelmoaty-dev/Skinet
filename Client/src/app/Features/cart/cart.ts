import { Component, inject } from '@angular/core';
import { CartService } from '../../Core/Services/cart-service';
import { CartItemComponent } from './cart-item/cart-item.component';
import { CartItem } from '../../Shared/models/cart';
import { OrderSummaryComponent } from "../../Shared/components/order-summary/order-summary.component";

@Component({
  selector: 'app-cart',
  imports: [CartItemComponent, OrderSummaryComponent],
  templateUrl: './cart.html',
  styleUrl: './cart.scss'
})
export class Cart {
  cartService=inject(CartService)
  increaseQuantity(cartItem:CartItem){
    this.cartService.addItemToCart(cartItem)
  }
  decreaseQuantity(productId:number){
    this.cartService.removeItemFromCart(productId)
  }
  deleteCartItem(productId:number){
    this.cartService.removeProductFromCart(productId)
  }
}
