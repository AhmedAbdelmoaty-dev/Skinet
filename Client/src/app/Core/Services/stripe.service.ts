import { inject, Injectable } from '@angular/core';
import{ConfirmationToken, loadStripe, Stripe, StripeAddressElement, StripeAddressElementOptions, StripeElements, StripePaymentElement} from '@stripe/stripe-js'
import { environment } from '../../../environments/environment';
import { CartService } from './cart-service';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, map } from 'rxjs';
import { Cart } from '../../Shared/models/cart';
import { AccountService } from './account.service';
@Injectable({
  providedIn: 'root'
})
export class StripeService {
  baseUrl=environment.apiUrl
  private stripePromise!:Promise<Stripe | null>
  private elements?:StripeElements
  private addressElement?:StripeAddressElement
  private paymentElement?:StripePaymentElement
  private accountService=inject(AccountService)
  cartService=inject(CartService)
  http=inject(HttpClient)
  constructor() {
    this.stripePromise=loadStripe(environment.stripePublicKey)
   }

  getStripeInstance(){
    return this.stripePromise
  }

  async createConfirmationToken(){
    const stripe= await this.getStripeInstance()
    const elements=await this.initializeElements()
    const result =await elements.submit() //validates all inputs in address element and payment
    if(result.error) throw new Error(result.error.message)

      if(stripe){
        return await stripe.createConfirmationToken({elements}) //creates the confirmation token
      }else{
        throw new Error ('Stripe not avaliable')
      }
  }
  
  async confirmPayment(confirmationToken?:ConfirmationToken){
    const stripe= await this.getStripeInstance()
    const elements=await this.initializeElements()
    const result =await elements.submit()
    if(result.error) throw new Error(result.error.message)

      const clientSecret=this.cartService.cart()?.clientSecret

      if(stripe&&clientSecret){
        return await stripe.confirmPayment({
          clientSecret:clientSecret,
          confirmParams:{
            confirmation_token:confirmationToken?.id
          },
          redirect:'if_required'
        })
      }else{
        throw Error('something went wrong')
      }
  }

  async initializeElements(){
    if(!this.elements){
      const stripe=await this.getStripeInstance();
      if(stripe){
        const cart =await firstValueFrom(this.createOrUpdatePaymentIntent())
        this.elements=stripe.elements({clientSecret:cart.clientSecret,appearance:{labels:'floating'}})
      }else{
        throw new Error("Stripe has not being loaded");
      }
    }
    return this.elements
  }

  createOrUpdatePaymentIntent(){
    const cart =this.cartService.cart()
    if(!cart) throw new Error("Proplem with cart")
      
      return this.http.post<Cart>(`${this.baseUrl}payment/${cart.id}`,{}).pipe(
        map(cart=>{
          this.cartService.cart.set(cart)
          return cart
        })
      )
  }


  async createAddressElements(){
    if(!this.addressElement){
      const elements= await this.initializeElements()
      if(elements){
        const user=this.accountService.currentUser()
        let defaultValues:StripeAddressElementOptions['defaultValues']={
        }
        if(user){
          defaultValues.name=user.firstName+' ' +user.lastName
        }
        if(user?.address){
          defaultValues.address={
            line1:user.address.line1,
            line2:user.address.line2,
            city:user.address.city,
            state:user.address.state,
            country:user.address.country,
            postal_code:user.address.postalCode
          }
        }
        const options:StripeAddressElementOptions={
          mode:'shipping',  // means address for shipping , stripe supports shipping , billing
          defaultValues
        }
        this.addressElement=elements.create('address',options)
      }else{
        throw new Error('Elements Instance has not been loaded')
      }
      
    }
    return this.addressElement
  }
  
  // payment element ep14
  async createPaymentElement(){
    if(!this.paymentElement){
      const elements= await this.initializeElements();
      if (elements){
        this.paymentElement=elements.create('payment')
      }else{
        throw new Error('elements instance has not been initialized')
      }
    }
    return this.paymentElement
  }
  // ----------
  
  disposeElemnets(){
    this.addressElement=undefined
    this.elements=undefined
    this.paymentElement=undefined
  }

  
}
