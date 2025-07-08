import { Component, input, output } from '@angular/core';
import { CartItem } from '../../../Shared/models/cart';
import { CurrencyPipe } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { Cart } from '../cart';

@Component({
  selector: 'app-cart-item',
  imports: [CurrencyPipe,MatIcon],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
  item=input<CartItem>()
  readonly increase=output<CartItem>()
  readonly decrease=output<number>()
  readonly delete=output<number>()
}
