import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../Core/Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../Shared/models/product';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-details',
  imports: [  CommonModule,
    MatButtonModule,
    MatIconModule,
    MatBadgeModule,
    MatChipsModule,
    MatDividerModule,
    MatSnackBarModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopService=inject(ShopService)
  private route=inject(ActivatedRoute)
  product?:Product
  ngOnInit(): void {
    this.LoadProduct()
  }
  LoadProduct(){
    const id =this.route.snapshot.paramMap.get('id')
    if(!id)return
    this.shopService.getProductById(+id).subscribe({
      next:result=>{console.log(result); this.product=result},
      error:err=>console.log(err)
    })
  }

  //
  selectedQuantity = 1;
  hasDiscount = false;
  originalPrice = 0;

  constructor(private snackBar: MatSnackBar) {}

  getStockStatus(): string {
    if(this.product){
    if (this.product.quantityInStock === 0) {
      return 'Out of Stock';
    } else if (this.product.quantityInStock < 10) {
      return `Only ${this.product.quantityInStock} left`;
    } 
  }
  return 'In Stock';
  }

  increaseQuantity(): void {
    if (this.product&& this.selectedQuantity < this.product.quantityInStock) {
      this.selectedQuantity++;
    }
  }

  decreaseQuantity(): void {
    if (this.selectedQuantity > 1) {
      this.selectedQuantity--;
    }
  }

  addToCart(): void {
    if (this.product&& this.product?.quantityInStock > 0) {
      // Add your cart logic here
      this.snackBar.open(
        `Added ${this.selectedQuantity} ${this.product?.name} to cart`, 
        'Close', 
        { duration: 3000 }
      );
    }
  }

  addToWishlist(): void {
    // Add your wishlist logic here
    this.snackBar.open(
      `${this.product?.name} added to wishlist`, 
      'Close', 
      { duration: 3000 }
    );
  }

  shareProduct(): void {
    // Add your share logic here
    if (navigator.share) {
      navigator.share({
        title: this.product?.name,
        text: this.product?.description,
        url: window.location.href
      });
    } else {
      // Fallback - copy to clipboard
      navigator.clipboard.writeText(window.location.href);
      this.snackBar.open('Product link copied to clipboard', 'Close', { duration: 3000 });
    }
  }

  onImageError(event: any): void {
    // Fallback image logic
    event.target.src = 'assets/images/placeholder-product.jpg';
  }
}
