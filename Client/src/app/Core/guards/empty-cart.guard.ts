import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { inject } from '@angular/core';
import { CartService } from '../Services/cart-service';
import { MatSnackBar } from '@angular/material/snack-bar'


export const emptyCartGuard: CanActivateFn = (route, state) => {
  const router=inject(Router)
  const cartService=inject(CartService)
  const snack =inject(MatSnackBar)

  if(!cartService.cart() || cartService.cart()?.items.length===0){
    snack.open('no items in the cart')
    router.navigateByUrl('/cart')
    return false
  }else{
    return true;
  }
};
