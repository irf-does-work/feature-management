import { NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DialogComponent } from '../dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { FeatureStatus, FeatureType } from '../enum/feature.enum';
import { IFeature, IBusiness, IUpdateToggle } from '../interface/feature.interface';
import { FeatureService } from '../feature.service';
import { error } from 'console';
import { response } from 'express';




@Component({
  selector: 'app-feature-card',
  standalone: true,
  imports: [NgClass, RouterModule],
  templateUrl: './feature-card.component.html',
  styleUrls: ['./feature-card.component.scss']
})

export class FeatureCardComponent {
  isAdmin: number = 0;
  currentUser: string | undefined;

  constructor(public dialog: MatDialog,
    private featureService: FeatureService
  ) {
    const payload = this.featureService.decodeToken();

    payload.IsAdmin === "True" ? this.isAdmin = 1 : this.isAdmin = 0;

    this.currentUser = payload.UserID

    console.log("payload admin" + payload.IsAdmin)
    console.log("feature-card admin" + this.isAdmin)
    console.log("Current UserID: " + this.currentUser)
  }

  featureTypeEnum = FeatureType;
  featureStatusEnum = FeatureStatus;
  features: IFeature[] = [
    { FeatureId: 19, name: 'Invoice Generation', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 56, name: 'Tax Calculation', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { FeatureId: 3, name: 'Fraud Detection', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 4, name: 'Transaction History', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { FeatureId: 5, name: 'Mobile Payment', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { FeatureId: 6, name: 'Currency Exchange', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 7, name: 'Batch Processing', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 57, name: 'Verify Bank', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { FeatureId: 9, name: 'Invoice Template', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 10, name: 'Automated Payment', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 11, name: 'Audit Logs', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 12, name: 'Report Generator', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 13, name: 'User Analytics', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { FeatureId: 14, name: 'Data Sync', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { FeatureId: 15, name: 'Payment Gateway', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 16, name: 'Currency Conversion', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { FeatureId: 17, name: 'Fraud Detection V2', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { FeatureId: 18, name: 'Billing Cycle', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 100, name: 'Account Management', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { FeatureId: 20, name: 'Dynamic Pricing', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 21, name: 'Credit Score Check', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 22, name: 'Customer Feedback', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { FeatureId: 23, name: 'Live Chat Support', type: FeatureType.Feature, status: FeatureStatus.Disabled },
    { FeatureId: 24, name: 'Data Encryption', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { FeatureId: 25, name: 'API Throttling', type: FeatureType.Release, status: FeatureStatus.Disabled },
    { FeatureId: 26, name: 'Mobile Notifications', type: FeatureType.Feature, status: FeatureStatus.Enabled },
    { FeatureId: 27, name: 'Transaction Security', type: FeatureType.Release, status: FeatureStatus.Enabled },
    { FeatureId: 28, name: 'Password Reset', type: FeatureType.Feature, status: FeatureStatus.Disabled }
  ];




  // businesses: IBusiness[] = [
  //   { name: 'Business 1', businessId: '1', status: FeatureStatus.Enabled },
  //   { name: 'Business 2', businessId: '2', status: FeatureStatus.Disabled },
  //   { name: 'Business 3', businessId: '3', status: FeatureStatus.Disabled },
  //   { name: 'Business 4', businessId: '4', status: FeatureStatus.Enabled },
  //   { name: 'Business 5', businessId: '5', status: FeatureStatus.Enabled },
  //   { name: 'Business 6', businessId: '6', status: FeatureStatus.Disabled },
  //   { name: 'Business 7', businessId: '7', status: FeatureStatus.Disabled },
  //   { name: 'Business 8', businessId: '8', status: FeatureStatus.Enabled },
  //   { name: 'Business 9', businessId: '9', status: FeatureStatus.Enabled },
  //   { name: 'Business 10', businessId: '10', status: FeatureStatus.Disabled },

  // ];


  business: string | undefined;
  name: string | undefined;




  itemsPerPage: number = 12;
  currentPage: number = 1;

  get paginatedFeatures(): IFeature[] {
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


  openDialog(action: FeatureStatus.Enabled | FeatureStatus.Disabled, featureId: number): void {

    const apiEndpoint = action === FeatureStatus.Enabled
      ? `/api/Business/Enable`
      : `/api/Business/Disable`;


    // Call the API to fetch businesses
    this.featureService.getBusinesses(apiEndpoint, featureId).subscribe({
      next: (response: IBusiness[]) => {
        // Open the dialog with the fetched businesses
        console.log(response);
        const dialogRef = this.dialog.open(DialogComponent, {
          width: '20%',
          data: {
            businesses: response
          }
        });

        // Handle dialog close
        dialogRef.afterClosed().subscribe((result: IBusiness | null) => {
          if (result) {
            this.business = result.businessId;
            console.log('Selected Business:', result);
            this.update_Toggle(featureId, Number(result.businessId), action); // Pass the businessId and action to update_Toggle
          }
        });
      },
      error: (error) => {
        console.error('Error fetching businesses:', error);
        alert('Failed to load businesses. Please try again.');
      }
    });
  }


  // const filteredBusinesses = this.businesses
  //   .filter(feature => action === FeatureStatus.Enabled ? feature.status === FeatureStatus.Disabled : feature.status === FeatureStatus.Enabled)
  //   .map(business => ({ name: business.name, businessId: business.businessId, status: business.status }));

  // let dialogRef = this.dialog.open(DialogComponent, {
  //   width: '20%',
  //   data: {
  //     // name: this.name, id: this.business, 
  //     businesses: filteredBusinesses
  //   }
  // });

  // dialogRef.afterClosed().subscribe((result: IBusiness | null) => {
  //   if (result) {
  //     this.business = result.name;
  //     console.log('Selected Business:', result);
  //     this.update_Toggle(featureId, Number(result.businessId), action); // Pass the businessId and action to update_Toggle

  //   }
  // });


  update_Toggle(featureId: number, businessId: number | null, featureStatus: string) {
    console.log(`Updating FeatureId: ${featureId}, BusinessId: ${businessId}, Status: ${featureStatus}, UserId: ${this.currentUser}`);

    if (featureStatus == this.featureStatusEnum.Enabled) {

    }
    const data: IUpdateToggle = {
      UserId: this.currentUser,
      featureId: featureId,
      businessId: businessId,
      enableOrDisable: featureStatus == this.featureStatusEnum.Enabled ? true : false

    }


    this.featureService.updateToggle(data).subscribe({
      next: (response: any) => {
        console.log(response)
      },
      error: (error) => {
        console.error('Error updating feature:', error);
      }
    });
  }

}
