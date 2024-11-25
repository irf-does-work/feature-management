import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { NavbarComponent } from '../navbar/navbar.component';
import { OnInit } from '@angular/core';
import { Ilog, IPaginationLog } from '../interface/feature.interface';
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
  pageNumber : number = 0;

  dataSource: IPaginationLog= {
    pageSize: 0,
    currentPage: 0,
    totalCount: 0,
    totalPages: 0,
    logs: []
  };
  
  constructor(private featureService: FeatureService) { }
  ngOnInit(): void {
      this.fetchPaginatedLog();
  }
  
  displayedColumns: string[] = ['serialNo', 'UserId', 'Username', 'FeatureId', 'FeatureName', 'BusinessId','BusinessName','Date','Time', 'Action'];




  

  fetchPaginatedLog(){
    this.featureService.getLog(this.pageNumber).subscribe({
      next:(response:IPaginationLog) =>{
        this.dataSource = response;
        console.log(this.dataSource);
      },
      error:(err) =>{
        console.log("Error while fetching Logs: ", err)
      }
    });
  }

  // get paginatedFeatures(): IPaginationLog {
  //   const startIndex = (this.pageNumber - 1) * this.dataSource.pageSizeitemsPerPage;
  //   return this.dataSource.slice(startIndex, startIndex + this.itemsPerPage);
  // }


  goToPage(page: number) {
    if (page >= 0 && page <= this.dataSource.totalPages) {
      this.pageNumber = page;
      this.fetchPaginatedLog();
    }
  }

  nextPage() {
    if (this.pageNumber < this.dataSource.totalPages - 1) {
      this.pageNumber++;
      this.fetchPaginatedLog();
    }
  }

  previousPage() {
    if (this.pageNumber > 0) {
      this.pageNumber--;
      this.fetchPaginatedLog();
    }
  }

}
