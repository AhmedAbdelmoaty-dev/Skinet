import { computed, inject, Injectable, signal } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Cart, CartItem } from '../../Shared/models/cart';
import { Product } from '../../Shared/models/product';
import { map } from 'rxjs';
import { DeliveryMethod } from '../../Shared/models/deliveryMethod';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  cart = signal<Cart | null>(null);
  selectedDelivery=signal<DeliveryMethod|null>(null)
  private http = inject(HttpClient);
  itemCount = computed(() => {
    return this.cart()?.items.reduce((acc, item) => acc + item.quantity, 0);
  });

  totals=computed(()=>{
    const cart=this.cart()
    if(!cart) return null
    const delivery=this.selectedDelivery();
    const discount=0
    const subtotal=cart.items.reduce((acc,item)=>acc+=item.quantity*item.price,0)
    const shipping =delivery? delivery.price : 0
    return {
      discount,
      shipping,
      subtotal,
      total:subtotal+shipping-discount
    }
  })

  getCart(id: string) {
    return this.http.get<Cart>(environment.apiUrl + 'cart/' + id).pipe(
      map((cart) => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  setCart(cart: Cart) {
    this.http.post<Cart>(environment.apiUrl + 'cart', cart).subscribe((val) => {
      this.cart.set(val);
    });
  }

  private createCart() {
    const cart = new Cart();
    localStorage.setItem('cartId', cart.id);
    return cart;
  }
  private isProduct(item: Product | CartItem): item is Product {
    return (item as Product).id !== undefined;
  }

  addItemToCart(item: Product | CartItem, quantity = 1) {
    const cart = this.cart() ?? this.createCart();
    if (this.isProduct(item)) {
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, quantity);
    this.setCart(cart);
  }
 private addOrUpdateItem(items: CartItem[], item: CartItem, quantity: number) {
    const index = items.findIndex((e) => e.productId === item.productId);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  private mapProductToCartItem(item: Product): CartItem {
    return {
      productId: item.id,
      price: item.price,
      productName: item.name,
      imageUrl: item.pictureUrl,
      quantity: 0,
      brand: item.brand,
      type: item.type,
    };
  }
  deleteCart(){
    this.http.delete(environment.apiUrl+'cart/' +this.cart()?.id).subscribe({
      next: ()=>{
        localStorage.removeItem('cartId')
        this.cart.set(null)
      }
    })
  }

  private updateCartAfterRemoval(cart:Cart,index:number){
    cart.items.splice(index,1)
    if(cart.items.length<=0){
        this.deleteCart();
      }else{
        this.setCart(cart)
      }
  }
  
  removeItemFromCart(productId:number,quantity=1){
    const cart=this.cart() 
    if(!cart) return
    const index=cart.items.findIndex(x=>x.productId===productId)
    if(index!==-1){
      if(cart.items[index].quantity>1){
        cart.items[index].quantity-=quantity
         this.setCart(cart);
      }else{
        this.updateCartAfterRemoval(cart,index)
      }
      
    }
  }

  removeProductFromCart(productId:number){
    const cart=this.cart();
    if(!cart) return
   const index= cart.items.findIndex(x=>x.productId===productId)
    this.updateCartAfterRemoval(cart,index)
  }

  
}
