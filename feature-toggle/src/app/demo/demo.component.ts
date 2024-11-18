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
  
  business?: Business; // Initialize as undefined
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

  // Store selected filter values
  selectedFilters: string[] = [];

  // Applies filters when the button is clicked
  applyFilters(): void {
    // Clear previously selected filters
    this.selectedFilters = [];

    // Select all checkboxes and gather their values if checked
    const checkboxes = document.querySelectorAll('.form-check-input');

    // checkboxes.forEach((checkbox: HTMLInputElement) => {
    //   if (checkbox.checked) {
    //     this.selectedFilters.push(checkbox.value);
    //   }
    // });


    checkboxes.forEach((checkbox) => {
      // Use type assertion to cast each checkbox as HTMLInputElement
      const inputElement = checkbox as HTMLInputElement;
      if (inputElement.checked) {
        this.selectedFilters.push(inputElement.value);
      }
    });

    console.log('Selected Filters:', this.selectedFilters);
    // Implement your filtering logic based on selectedFilters
  }
}
