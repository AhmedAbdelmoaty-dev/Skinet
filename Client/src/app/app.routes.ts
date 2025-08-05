import { Routes } from '@angular/router';
import { HomeComponent } from './Features/home/home.component';
import { ShopComponent } from './Features/shop/shop.component';
import { ProductDetailsComponent } from './Features/product-details/product-details.component';
import { NotFoundComponent } from './Shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './Shared/components/server-error/server-error.component';
import { TestErrorComponent } from './Features/test-error/test-error.component';
import { Cart } from './Features/cart/cart';
import { LoginComponent } from './Features/account/login/login.component';
import { RegisterComponent } from './Features/account/register/register.component';
import { CheckoutComponent } from './Features/checkout/checkout.component';
import { authGuard } from './Core/guards/auth.guard';
import { emptyCartGuard } from './Core/guards/empty-cart.guard';
import { CheckoutSuccessComponent } from './Features/checkout/checkout-success/checkout-success.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'shop', component: ShopComponent },
  { path: 'shop/:id', component: ProductDetailsComponent },
  { path: 'cart', component: Cart },
  {path:'checkout',component:CheckoutComponent,canActivate: [authGuard,emptyCartGuard]},
  {path:'checkout/success',component:CheckoutSuccessComponent,canActivate: [authGuard]},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  { path: 'test-error', component: TestErrorComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
