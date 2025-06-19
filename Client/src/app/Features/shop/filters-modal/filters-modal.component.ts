import {  Component, inject} from '@angular/core';
import {MatDivider} from '@angular/material/divider';
import { ShopService } from '../../../Core/Services/shop.service';
import {MatSelectionList,MatListOption} from '@angular/material/list';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-filters-modal',
  imports: [MatDivider,MatSelectionList,MatListOption,FormsModule],
  templateUrl: './filters-modal.component.html',
  styleUrl: './filters-modal.component.scss'
})
export class FiltersModalComponent {
  shopService=inject(ShopService)
  private dialogRef=inject(MatDialogRef<FiltersModalComponent>)
  data=inject(MAT_DIALOG_DATA)
  selectedBrands:string[]=this.data.selectedBrands
  selectedTypes:string[]=this.data.selectedTypes

  applyFilters(){
    
    this.dialogRef.close( {
      selectedBrands:this.selectedBrands,
      selectedTypes:this.selectedTypes
    } )
  }
}
