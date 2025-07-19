import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService=inject(AccountService)
  const router=inject(Router)
  if(accountService.currentUser())
    return true
  router.navigate(['/login'],{queryParams:{returnUrl:state.url}})
  return false

  //route is of type ActivatedRouteSnapshot it represents the segments
  //of the url user trying to access

  //while state is the full url the user is trying to access
};
