import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCard } from '@angular/material/card';
import { AccountService } from '../../../Core/Services/account.service';
import { Router } from '@angular/router';
import { MatError } from '@angular/material/form-field';
import { MatDivider } from "@angular/material/divider";


@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, MatCard, MatError, MatDivider],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
 private fb=inject(FormBuilder)
 private accountService=inject(AccountService)
 private router=inject(Router)
 validationErrors?: string []

 registerForm=this.fb.group({
  firstName:['',Validators.required],
  lastName:['',Validators.required],
  userName:['',Validators.required],
  email:['',[Validators.required,Validators.email]],
  password:['',[Validators.required,Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)]],
  confirmPassword:['',[Validators.required]]
 })

 onSubmit(){
   if (this.registerForm.invalid) return;
  this.accountService.register(this.registerForm.value).subscribe({
    next:()=>{
      this.accountService.getUserInfo().subscribe({
        next:()=>{
          this.router.navigateByUrl('/shop')
        }
      })
    },
    error: responseErrors=>{
    this.validationErrors=Object.values(responseErrors.error.errors).flat() as string[];
     }
      //httpErrorResponse->error->errors where errors object is the object coming from the backend 
      //httpErrorResponse and error are outer wrapers 
  })
 }

//  passwordMatchValidator(form:FormGroup){
//   const password=form.get('password')
//   const confirmPassword=form.get('confirmPassword')
//   if(password?.value!==confirmPassword?.value){
//     form.get('confirmPassword')?.setErrors({notMatch:true})
//   }else {
//     const errors = confirmPassword?.errors;
//     if (errors) {
//       delete errors['notMatch'];
//       if (Object.keys(errors).length === 0) {
//         confirmPassword.setErrors(null);
//         return  null
//       } else {
//         confirmPassword.setErrors(errors);
//         return {errors}
//       }
//     }
//   return null
//  }
// }
}
