import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import { Address, AuthDto, User } from '../../Shared/models/User';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http=inject(HttpClient)
  private baseUrl=environment.apiUrl+'account'
  currentUser=signal<User|null>(null)
  private router =inject(Router)
  login(values:any){
    let params=new HttpParams()
    params=params.append("useCookies",true)
   return  this.http.post<User>(this.baseUrl+'/login',values,{params}).pipe(
    tap(user=>{
      console.log(user)
      if(user.token){
        localStorage.setItem('accessToken',user.token)
      }
      if(user.refreshToken){
        localStorage.setItem('refreshToken',user.refreshToken)
      }
    })
   )
  }

  register(values:any){
    return this.http.post<User>(this.baseUrl+'/register',values).pipe(
      tap(user=>{
      if(user.token)
        localStorage.setItem('accessToken',user.token)
      if(user.refreshToken)
        localStorage.setItem('refreshToken',user.refreshToken)
    }))
  }

  getUserInfo(){
    return this.http.get<User>(this.baseUrl+'/userinfo',{params:{withAddress:true}}).pipe(
      tap(user=>this.currentUser.set(user))
    )
    
  }

  logout(){
    return this.http.post(this.baseUrl+'/logout',{
      refreshtoken:localStorage.getItem('refreshToken')
    }).pipe(
      tap(()=>{
        localStorage.removeItem('accessToken')
        localStorage.removeItem('refreshToken')
      })
    )
  }

  updateAddress(address:Address){
    return this.http.post(this.baseUrl+'/address',address).pipe(
      tap(()=>{
        this.currentUser.update(user=>{
          if(user) user.address=address
          return user
        })
      })
    )
  }

  RequestUpdateAccessToken(){
    const rToken=localStorage.getItem('refreshToken')
    return this.http.post<AuthDto>(this.baseUrl+'/refresh-token',{refreshToken:rToken})
  }
}


