import { Component, inject, input } from '@angular/core';
import { CartService } from '../../../Core/Services/cart-service';
import { CurrencyPipe } from '@angular/common';
import { ConfirmationToken } from '@stripe/stripe-js';
import { AddressPipe } from '../../../Shared/pipes/address.pipe';
import { PaymentCardPipe } from '../../../Shared/pipes/payment-card.pipe';

@Component({
  selector: 'app-checkout-review',
  imports: [CurrencyPipe,AddressPipe,PaymentCardPipe],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss'
})
export class CheckoutReviewComponent {
  cartService=inject(CartService)
  confirmationToken=input<ConfirmationToken>()
}
