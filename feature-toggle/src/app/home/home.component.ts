import { Component, EventEmitter, Output } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FeatureCardComponent } from '../feature-card/feature-card.component';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IFilterForm, IselectedFilters } from '../interface/feature.interface';
import { FeatureService } from '../feature.service';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent, FeatureCardComponent, FormsModule, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  //enabledCheckboxSate : boolean = false;
  //disabledCheckboxSate : boolean = false; 

  selectedFiltersForm = new FormGroup<IFilterForm>({
    searchQuery: new FormControl<string | null>(''),
    featureToggleFilter: new FormControl<boolean | null>(false),
    releaseToggleFilter: new FormControl<boolean | null>(false),
    enabledFilter: new FormControl<boolean | null>(false),
    disabledFilter: new FormControl<boolean | null>(false),
  });
  
  @Output() applyFiltersEvent = new EventEmitter<IselectedFilters>(); //
  constructor(private featureService: FeatureService) { }

  applyFilters(): void {
    console.log("filters applied: ", this.selectedFiltersForm.value );
  }

  rtOptionsCheck(){
    // if(!this.rtCheckboxSate){
    //   this.enabledCheckboxSate = false;
    //   this.disabledCheckboxSate = false;
    // }
    if(!this.selectedFiltersForm.value.releaseToggleFilter){
      this.selectedFiltersForm.value.enabledFilter = false;
      this.selectedFiltersForm.value.disabledFilter = false;

    }
  }

  clearRtOptions(){
    this.selectedFiltersForm.value.releaseToggleFilter = false


  }

  removeFilters(){
    this.selectedFiltersForm.reset();
  }
}
