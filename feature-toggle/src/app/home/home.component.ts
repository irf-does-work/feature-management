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
    //logic
  }
}
