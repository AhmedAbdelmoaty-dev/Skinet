import { nanoid } from 'nanoid';
export interface CartType {
  id: string;
  items: CartItem[];
  deliveryMethodId?:number;
  paymentIntentId?:string;
  clientSecret?:string
}
export interface CartItem {
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  type: string;
  brand: string;
  imageUrl: string;
}

export class Cart implements CartType {
  id = nanoid();
  items: CartItem[] = [];
  deliveryMethodId?:number;
  paymentIntentId?:string;
  clientSecret?:string
}
