import { CurrencyPipe, Location } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import { CartService } from '../../../Core/Services/cart-service';
@Component({
  selector: 'app-order-summary',
  imports: [CurrencyPipe,RouterLink,MatButton,MatFormField,MatLabel,MatInput],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.scss'
})
export class OrderSummaryComponent {
  cartService=inject(CartService)
  location=inject(Location)
}
