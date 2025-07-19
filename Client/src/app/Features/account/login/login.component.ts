import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../../../Core/Services/account.service';
import { Router } from '@angular/router';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { EmptyStateComponent } from '../../../Shared/components/empty-state/empty-state.component';

@Component({
  selector: 'app-login',
  imports: [MatCard,ReactiveFormsModule,MatFormField,MatLabel,EmptyStateComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  fb=inject(FormBuilder)
  router=inject(Router)
  accountService=inject(AccountService)
  loginForm=this.fb.group({
    email:[""],
    password:[""]
  })

  onSubmit(){

    this.accountService.login(this.loginForm.value).subscribe({
      next:()=>{
        this.accountService.getUserInfo().subscribe({
          next:()=>this.router.navigateByUrl('/shop'),
        });
      
      },
    })
  }

}
