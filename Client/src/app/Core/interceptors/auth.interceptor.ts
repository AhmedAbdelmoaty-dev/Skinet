import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { catchError, map, mergeMap, switchMap, tap } from 'rxjs/operators';
import { AccountService } from '../Services/account.service';
import { AuthDto } from '../../Shared/models/User';
import { inject } from '@angular/core';
import { throwError } from 'rxjs';


export const authInterceptor: HttpInterceptorFn = (req, next) => {
  //req is the request you send 
  //you typically clone the original request and modify it here we 
  //allow using cookie in request then path the cloned modified version
  //to the pipeline inested of original one
  //we do this because http request is immutable so we need to clone it
  //and pass the modified one
  const accountService=inject(AccountService)
  let accessToken=localStorage.getItem('accessToken')
  const clonedRequest=req.clone({
    setHeaders:{
     'Authorization': `Bearer ${accessToken}`
    },
    withCredentials:true
  })
  return next(clonedRequest).pipe(
    catchError(err=>{
      if(err instanceof HttpErrorResponse &&err.status===401){
          return handle401Error(req,next,accountService )
      }
      return throwError(()=>err)
    })
  );
};
function handle401Error(request:HttpRequest<unknown>,next:HttpHandlerFn,accountService:AccountService){
  return accountService.RequestUpdateAccessToken().pipe(
    tap((res:AuthDto)=>{
      localStorage.setItem('accessToken',res.token)
    }),
    switchMap((res:AuthDto)=>{
      const retryRequest=request.clone({
        setHeaders:{
          'Authorization': `Bearer ${res.token}`
        },
        withCredentials:true
      })
      return next(retryRequest)
    })
    
  )
}