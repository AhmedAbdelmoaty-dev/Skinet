import { Component, inject } from '@angular/core';
import { Product } from '../../Shared/models/product';
import { ShopService } from '../../Core/Services/shop.service';
import {MatCard} from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatIcon } from '@angular/material/icon';
import {MatDialog} from '@angular/material/dialog';
import { FiltersModalComponent } from './filters-modal/filters-modal.component';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import {MatMenu, MatMenuTrigger} from '@angular/material/menu';
import { ShopParams } from '../../Shared/models/shopParams';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import { Pagination } from '../../Shared/models/pagination';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-shop',
  imports: [
    MatCard,
    ProductItemComponent,
    MatIcon,
    MatSelectionList,
    MatListOption,
    MatMenu,
    MatMenuTrigger,
    MatPaginator,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent {
   shopService=inject(ShopService)
   diaglogService=inject(MatDialog)
  url="https://localhost:7274/api/"
  products?:Pagination<Product>
  shopParams=new ShopParams()
  pageSizes=[5,10,20,50]
  sortOptions=[
    {name:'Alphapetical', value:'name'},
    {name:'Price: Low-High', value:'priceAsc'},
    {name:'Price: High-Low', value:'priceDesc'}
  ]
  ngOnInit(): void {
    this.initializeShop();
  }
  initializeShop(){
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
  }
  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next:response=> { this.products=response},
      error:(err)=>console.log(err)
    })
  }

  onSortChange(event:MatSelectionListChange){
    const selectedOption=event.options[0]
    if(selectedOption){
      this.shopParams.sort=selectedOption.value
      this.shopParams.pageNumber=1
      this.shopService.getProducts(this.shopParams).
      subscribe(result=>this.products=result)
    }
  }
  handlePageEvent(event:PageEvent){
    this.shopParams.pageSize=event.pageSize
    this.shopParams.pageNumber=event.pageIndex+1
    this.getProducts()
  }
  onSearchChange(){
    this.shopParams.pageNumber=1
    this.getProducts()
  }

  openFilterDialog(enterAnimationDuration:string,exitAnimationDuration:string){
    const dialogRef=this.diaglogService.open(FiltersModalComponent,{
      minWidth:"500px",
      enterAnimationDuration,
      exitAnimationDuration,
      data:{
        selectedTypes:this.shopParams.types,
        selectedBrands:this.shopParams.brands
      }
    })
    dialogRef.afterClosed().subscribe(result=>{
        this.shopParams.brands=result.selectedBrands
        this.shopParams.types=result.selectedTypes
        this.shopParams.pageNumber=1
        this.shopService.getProducts(this.shopParams).subscribe({
          next:result=> this.products=result,
          error:error=>console.log(error)
        })
    })
  }
}
