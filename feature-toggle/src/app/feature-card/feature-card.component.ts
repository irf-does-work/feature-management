import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FeatureService } from '../feature.service';
import { DialogComponent } from '../dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { FeatureStatus, FeatureType } from '../enum/feature.enum';
import { Feature } from '../interface/feature.interface';


interface Business {
  name: string;
  businessId: string;
  status: FeatureStatus;
}


@Component({
  selector: 'app-feature-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './feature-card.component.html',
  styleUrls: ['./feature-card.component.scss']
})


export class FeatureCardComponent {
  
  constructor(public dialog: MatDialog, ) {}
  
  isAdmin = 1;

  featureTypeEnum = FeatureType;  
  featureStatusEnum = FeatureStatus; 
  features: Feature[] = [
    { name: 'Invoice Generation', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Tax Calculation', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Fraud Detection', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Transaction History', type: FeatureType.Release, status: FeatureStatus.Enabled},
    { name: 'Mobile Payment', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Currency Exchange', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Batch Processing', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'Verify Bank', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Invoice Template', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Automated Payment', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Audit Logs', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'Report Generator', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'User Analytics', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Data Sync', type:FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Payment Gateway', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Currency Conversion', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Fraud Detection V2', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Billing Cycle', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Account Management', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Dynamic Pricing', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Credit Score Check', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'Customer Feedback', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Live Chat Support', type: FeatureType.Feature, status: FeatureStatus.Disabled},
    { name: 'Data Encryption', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'API Throttling', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Mobile Notifications', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Transaction Security', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Password Reset', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Invoice Generation', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Tax Calculation', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Fraud Detection', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Transaction History', type: FeatureType.Release, status: FeatureStatus.Enabled},
    { name: 'Mobile Payment', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Currency Exchange', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Batch Processing', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'Verify Bank', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Invoice Template', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Automated Payment', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Audit Logs', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'Report Generator', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'User Analytics', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Data Sync', type:FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Payment Gateway', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Currency Conversion', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { name: 'Fraud Detection V2', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Billing Cycle', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Account Management', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Dynamic Pricing', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Credit Score Check', type: FeatureType.Release, status: FeatureStatus.Disabled},
    { name: 'Customer Feedback', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Live Chat Support', type: FeatureType.Feature, status: FeatureStatus.Disabled},
    { name: 'Data Encryption', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'API Throttling', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { name: 'Mobile Notifications', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { name: 'Transaction Security', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { name: 'Password Reset', type: FeatureType.Feature, status: FeatureStatus.Disabled }
  ];

  businesses: Business[] = [
    {name: 'Business 1', businessId: '1',status: FeatureStatus.Enabled},
    {name: 'Business 2', businessId: '2', status: FeatureStatus.Disabled},
    {name: 'Business 3', businessId: '3', status: FeatureStatus.Disabled},
    {name: 'Business 4', businessId: '4',status: FeatureStatus.Enabled},
    {name: 'Business 5', businessId: '5',status: FeatureStatus.Enabled},
    {name: 'Business 6', businessId: '6',status: FeatureStatus.Disabled},
    {name: 'Business 7', businessId: '7',status: FeatureStatus.Disabled},
    {name: 'Business 8', businessId: '8',status: FeatureStatus.Enabled},
    {name: 'Business 9', businessId: '9',status: FeatureStatus.Enabled},
    {name: 'Business 10', businessId: '10',status: FeatureStatus.Disabled},

  ];


  business: string | undefined; 
  name: string | undefined; 




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

  // openDialog(action: FeatureStatus.Enabled | FeatureStatus.Disabled): void { 

  //   // const filteredFeatures = this.features.filter(feature =>
  //   //   action === FeatureStatus.Enabled ? feature.status === FeatureStatus.Disabled : feature.status === FeatureStatus.Enabled
  //   // );


  //   const filteredFeatures = this.businesses
  //       .filter(feature => action === FeatureStatus.Enabled ? feature.status === FeatureStatus.Disabled : feature.status === FeatureStatus.Enabled)
  //       .map(feature => ({ 
  //         // name: feature.name,
  //          status: feature.status 
  //         }));

  //   // const filteredFeatures = this.features
  //   // .filter(feature => FeatureStatus.Enabled ? feature.status === FeatureStatus.Disabled : feature.status === FeatureStatus.Enabled)
  //   // .map(feature => ({ name: feature.name, status: feature.status }));

  //   console.log(filteredFeatures)

  //   let dialogRef = this.dialog.open(DialogComponent, { 
  //     width: '20%', 
  //     data: { name: this.name, id: this.business ,status: filteredFeatures } 
  //   }); 
  
  //   dialogRef.afterClosed().subscribe(result => { 
  //     this.business = result; 
  //     console.log(this.business)
  //   }); 
    
  // } 


  openDialog(action: FeatureStatus.Enabled | FeatureStatus.Disabled): void { 
    const filteredBusinesses = this.businesses
        .filter(business => action === FeatureStatus.Enabled ? business.status === FeatureStatus.Disabled : business.status === FeatureStatus.Enabled)
        .map(business => ({ name: business.name, businessId: business.businessId, status: business.status }));

    let dialogRef = this.dialog.open(DialogComponent, { 
        width: '20%', 
        data: { 
          // name: this.name, id: this.business, 
          businesses: filteredBusinesses } 
    }); 

    dialogRef.afterClosed().subscribe((result: Business | null) => { 
        if (result) {
            this.business = result.businessId; 
            console.log('Selected Business:', result);
        }
    }); 
}

}
