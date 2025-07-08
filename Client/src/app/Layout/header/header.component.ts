import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatBadge } from '@angular/material/badge';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatProgressBar } from '@angular/material/progress-bar';
import { BusyService } from '../../Core/Services/busy.service';
import { CartService } from '../../Core/Services/cart-service';
@Component({
  selector: 'app-header',
  imports: [
    MatButton,
    MatIcon,
    MatBadge,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatProgressBar,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  busyService = inject(BusyService);
  cartService = inject(CartService);
}
