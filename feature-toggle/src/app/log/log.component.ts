import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { NavbarComponent } from '../navbar/navbar.component';
import { OnInit } from '@angular/core';
import { Ilog } from '../interface/feature.interface';
import { FeatureService } from '../feature.service';
import { response } from 'express';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-log',
  standalone: true,
  imports: [NavbarComponent,MatTableModule,DatePipe],
  templateUrl: './log.component.html',
  styleUrl: './log.component.scss'
})
export class LogComponent implements OnInit {

  dataSource: Ilog[] = []
  
  constructor(private featureService: FeatureService) { }
  ngOnInit(): void {
      this.featureService.getLog().subscribe({
        next:(response:Ilog[]) =>{
          this.dataSource = response;
          console.log(this.dataSource);
        },
        error:(err) =>{
          console.log("Error while fetching Logs: ", err)
        }
      });
  }
  
  sampleDataSource = [
    {
      Id: 1,
      UserId: "12345",
      Username: "johndoe",
      FeatureId: 101,
      FeatureName: "Invoice Generation",
      BusinessId: "B001",
      BusinessName: "Acme Corp",
      Time: "2024-11-21T14:30:00Z",
      Action: "Feature Enabled"
    },
    {
      Id: 2,
      UserId: "67890",
      Username: "janedoe",
      FeatureId: 102,
      FeatureName: "Tax Calculation",
      BusinessId: "B002",
      BusinessName: "Global Tech",
      Time: "2024-11-21T15:00:00Z",
      Action: "Feature Disabled"
    }
  ];
  displayedColumns: string[] = ['serialNo', 'UserId', 'Username', 'FeatureId', 'FeatureName', 'BusinessId','BusinessName','Date','Time', 'Action'];
}
