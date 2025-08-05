import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { DeliveryMethod } from '../../Shared/models/deliveryMethod';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

   baseUrl=environment.apiUrl
  http=inject(HttpClient)

  deliveryMethods:DeliveryMethod[]=[]

  getDeliveryMethods(){
    if(this.deliveryMethods.length>0) return of(this.deliveryMethods)

      return this.http.get<DeliveryMethod[]>(this.baseUrl+'payment/delivery-methods').pipe(
        map(methods=>{
     this.deliveryMethods= methods.sort((a,b)=>b.price-a.price)
          console.log(this.deliveryMethods)
          return this.deliveryMethods
        })
      )
  }
}
