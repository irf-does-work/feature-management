import { Component, OnInit } from '@angular/core';
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
      this.isOpen = true;
    });

    this.drawerService.drawerClose$.subscribe(() => {
      this.isOpen = false;
    });
  }

  toggleDrawer() {
    if (this.isOpen) {
      this.drawerService.closeDrawer();
    } else {
      this.drawerService.openDrawer();
    }
  }
  
}
