import { ChangeDetectorRef, Component,Inject } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'; 
import { MatCommonModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';


import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';


interface IBusiness {
  name: string;
  sound: string;
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


  businessControl = new FormControl<IBusiness | null>(null, Validators.required);
  selectFormControl = new FormControl('', Validators.required);
  businesses: IBusiness[] = [
    {name: 'Business 1', sound: 'this is business 1'},
    {name: 'Business 2', sound: 'this is business 2'},
    {name: 'Business 3', sound: 'this is business 3'},
    {name: 'Business 4', sound: 'this is business 4'},
    {name: 'Business 5', sound: 'this is business 5'},
    {name: 'Business 6', sound: 'this is business 6'},
    {name: 'Business 7', sound: 'this is business 7'},
    {name: 'Business 8', sound: 'this is business 8'},
    {name: 'Business 9', sound: 'this is business 9'},
    {name: 'Business 10', sound: 'this is business 10'},

  ];



  
  onCancel(): void { 
    this.dialogRef.close(); 
  } 

}
