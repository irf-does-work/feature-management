import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FeatureCardComponent } from '../feature-card/feature-card.component';
import { FeatureStatus, FeatureType } from '../enum/feature.enum';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent, FeatureCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
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
