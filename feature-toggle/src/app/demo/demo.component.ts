import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DialogComponent } from '../dialog/dialog.component';

import { MatDialog } from '@angular/material/dialog';
import { FeatureStatus, FeatureType } from '../enum/feature.enum';



interface Business {
  name: string;
  sound: string;
}

@Component({
  selector: 'app-demo',
  standalone: true,
  imports: [
    CommonModule,
    
  ],
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.scss'
})

export class DemoComponent {
  
  business?: Business; 
  constructor(public dialog: MatDialog) {} 
  
  openDialog(): void { 
    let dialogRef = this.dialog.open(DialogComponent, { 
      width: '20%',
      data: { name: this.business?.name, sound: this.business?.sound } 
    }); 
  
    dialogRef.afterClosed().subscribe(result => { 
      this.business = result; 
      console.log(result);
    }); 
  } 

  featureTypeEnum = FeatureType;  
  featureStatusEnum = FeatureStatus; 

  selectedFilters: string[] = [];

  applyFilters(): void {
    this.selectedFilters = [];

    const checkboxes = document.querySelectorAll('.form-check-input');

    checkboxes.forEach((checkbox) => {
      const inputElement = checkbox as HTMLInputElement;
      if (inputElement.checked) {
        this.selectedFilters.push(inputElement.value);
      }
    });

    console.log('Selected Filters:', this.selectedFilters);
  }
}
