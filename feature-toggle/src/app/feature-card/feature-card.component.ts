import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DrawerComponent } from '../drawer/drawer.component';
import { FeatureService } from '../feature.service';

interface Feature {
  name: string;
  type: 'feature' | 'release';
  status: 'enabled' | 'disabled';
}

@Component({
  selector: 'app-feature-card',
  standalone: true,
  imports: [CommonModule, RouterModule, DrawerComponent],
  templateUrl: './feature-card.component.html',
  styleUrls: ['./feature-card.component.scss']
})
export class FeatureCardComponent {
  features: Feature[] = [
    { name: 'Invoice Generation', type: 'release', status: 'enabled' },
    { name: 'Tax Calculation', type: 'feature', status: 'disabled' },
    { name: 'Fraud Detection', type: 'release', status: 'disabled' },
    { name: 'Transaction History', type: 'release', status: 'enabled' },
    { name: 'Mobile Payment', type: 'release', status: 'enabled' },
    { name: 'Currency Exchange', type: 'release', status: 'disabled' },
    { name: 'Batch Processing', type: 'release', status: 'disabled' },
    { name: 'Verify Bank', type: 'feature', status: 'disabled' },
    { name: 'Invoice Template', type: 'release', status: 'disabled' },
    { name: 'Automated Payment', type: 'release', status: 'disabled' },
    { name: 'Audit Logs', type: 'release', status: 'disabled' },
    { name: 'Report Generator', type: 'release', status: 'disabled' },
    { name: 'User Analytics', type: 'feature', status: 'enabled' },
    { name: 'Data Sync', type: 'release', status: 'enabled' },
    { name: 'Payment Gateway', type: 'release', status: 'disabled' },
    { name: 'Currency Conversion', type: 'feature', status: 'disabled' },
    { name: 'Fraud Detection V2', type: 'release', status: 'enabled' },
    { name: 'Billing Cycle', type: 'release', status: 'disabled' },
    { name: 'Account Management', type: 'feature', status: 'enabled' },
    { name: 'Dynamic Pricing', type: 'release', status: 'disabled' },
    { name: 'Credit Score Check', type: 'release', status: 'disabled' },
    { name: 'Customer Feedback', type: 'feature', status: 'enabled' },
    { name: 'Live Chat Support', type: 'feature', status: 'disabled' },
    { name: 'Data Encryption', type: 'release', status: 'enabled' },
    { name: 'API Throttling', type: 'release', status: 'disabled' },
    { name: 'Mobile Notifications', type: 'feature', status: 'enabled' },
    { name: 'Transaction Security', type: 'release', status: 'enabled' },
    { name: 'Password Reset', type: 'feature', status: 'disabled' }
  ];


  isOpen = false;


  constructor(public drawerService: FeatureService) {}


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


  itemsPerPage: number = 12;
  currentPage: number = 1;

  get paginatedFeatures(): Feature[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.features.slice(startIndex, startIndex + this.itemsPerPage);
  }

  totalPages(): number {
    return Math.ceil(this.features.length / this.itemsPerPage);
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage = page;
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages()) {
      this.currentPage++;
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}
