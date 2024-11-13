import { Component, inject, OnInit } from '@angular/core';
import { FeatureService } from '../feature.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-drawer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './drawer.component.html',
  styleUrl: './drawer.component.scss'
})
export class DrawerComponent{
  isOpen = false;

  constructor(public drawerService: FeatureService) {}




  ngOnInit(): void {
    this.drawerService.drawerOpen$.subscribe(() => {
      console.log('opened');
      this.isOpen = true;
    });

    this.drawerService.drawerClose$.subscribe(() => {
      this.isOpen = false;
    });
  }

  openDrawer() {
    this.drawerService.open=true;
    console.log(this.drawerService.open)

    this.drawerService.openDrawer();
    
  }

  closeDrawer() {
    this.drawerService.open=false;
    console.log(this.drawerService.open)

    this.drawerService.closeDrawer();
  }


  toggleDrawer() {
    if (this.isOpen) {
      this.drawerService.closeDrawer();
    } else {
      this.drawerService.openDrawer();
    }
  }
  
}
