import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../Core/Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../../Core/Services/cart-service';
import { CurrencyPipe } from '@angular/common';
import { Product } from '../../Shared/models/product';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatDivider } from '@angular/material/divider';
import { FormsModule } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-product-details',
  standalone:true,
  imports: [ CurrencyPipe,MatFormField,MatDivider,MatLabel,FormsModule,MatIcon,MatButton],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit   {
  
  shopService=inject(ShopService)
  cartService=inject(CartService)
  product!:Product;
  quantityInCart=0   
  quantity=1                
  router=inject(ActivatedRoute)


  ngOnInit(): void {
  const  id=this.router.snapshot.paramMap.get('id')!
      this.shopService.getProductById(+id).subscribe({
        next:(value)=>{
          this.product=value;
          this.updateQuantityInCart()
        }
      })
  }
  
  updateQuantityInCart(){
   this.quantityInCart= this.cartService.cart()?.items.find(x=>x.productId===this.product.id)?.quantity||0
    this.quantity=this.quantityInCart
  }
  updateCart(){
    if(!this.product) return
    if (this.quantity>this.quantityInCart){
      const itemsToAdd=this.quantity-this.quantityInCart
      this.cartService.addItemToCart(this.product,itemsToAdd)
      this.quantityInCart=this.quantity
    }else{
      const itemsToRemove=this.quantityInCart-this.quantity
      this.cartService.removeItemFromCart(this.product.id,itemsToRemove)
      this.quantityInCart=this.quantity
    }
  }
  getButtonText(){
   return this.quantity>0&&this.quantityInCart>0 ? "Update Cart":"Add to cart"
  }
  
}
