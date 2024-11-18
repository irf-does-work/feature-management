import { ChangeDetectorRef, Component,Inject } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'; 
import { MatCommonModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';


import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';


interface Business {
  name: string;
  businessId: string;
}

@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [
    FormsModule, 
    MatButtonModule, 
    MatCommonModule, 
    MatDialogModule, 
    MatFormFieldModule, 
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.scss'
})



export class DialogComponent {
  constructor( 
    public dialogRef: MatDialogRef<DialogComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: any
    // private cd : ChangeDetectorRef
  ) { } 


  businessControl = new FormControl<Business | null>(null, Validators.required);
  selectFormControl = new FormControl('', Validators.required);
  businesses: Business[] = [
    {name: 'Business 1', businessId: '1'},
    {name: 'Business 2', businessId: '2'},
    {name: 'Business 3', businessId: '3'},
    {name: 'Business 4', businessId: '4'},
    {name: 'Business 5', businessId: '5'},
    {name: 'Business 6', businessId: '6'},
    {name: 'Business 7', businessId: '7'},
    {name: 'Business 8', businessId: '8'},
    {name: 'Business 9', businessId: '9'},
    {name: 'Business 10', businessId: '10'},

  ];



  
  onCancel(): void { 
    this.dialogRef.close(); 
  } 

}
