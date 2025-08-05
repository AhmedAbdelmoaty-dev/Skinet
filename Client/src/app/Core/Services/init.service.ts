import { inject, Injectable } from '@angular/core';
import { CartService } from './cart-service';
import { forkJoin, Observable, of } from 'rxjs';
import { AccountService } from './account.service';
import { Cart } from '../../Shared/models/cart';
import { User } from '../../Shared/models/User';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private cartService = inject(CartService);
  private accountService=inject(AccountService)
  init():Observable<{ cart: Cart | null, user: User | null }> {
    const cartId = localStorage.getItem('cartId');
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);
    const userInfo$=this.accountService.getUserInfo();
    if(localStorage.getItem('refreshToken')){
      return forkJoin({cart:cart$,user:userInfo$});
    }
     return forkJoin({cart:cart$,user:of(null)});
  }
}
