import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DialogComponent } from '../dialog/dialog.component';

import { MatDialog } from '@angular/material/dialog';



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
}
