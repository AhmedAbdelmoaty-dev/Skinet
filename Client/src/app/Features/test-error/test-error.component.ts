import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  imports: [MatButton],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
  http=inject(HttpClient)
  url='https://localhost:7274/api/Error/'
  get400Error(){
    this.http.get(this.url+'bad-request').subscribe({
      next:result=>console.log(result),
      error:err=>console.log(err)
    })
  }
  get401Error(){
    this.http.get(this.url+'unauthorized').subscribe({
      next:result=>console.log(result),
      error:err=>console.log(err)
    })
  }
  get404Error(){
    this.http.get(this.url+'not-found').subscribe({
      next:result=>console.log(result),
      error:err=>console.log(err)
    })
  }
  get500Error(){
    this.http.get(this.url+'server-error').subscribe({
      next:result=>console.log(result),
      error:err=>console.log(err)
    })
  }
}
