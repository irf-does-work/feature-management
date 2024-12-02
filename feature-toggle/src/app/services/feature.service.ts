import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment';
import { IBusiness, IPaginatedFeatures, IPaginationLog, IselectedFilters, IUpdateToggle } from '../interface/feature.interface';




@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private baseUrl = environment.apiUrl;
  public userId: number = 0;

  constructor(private router: Router, private http: HttpClient,private toastr: ToastrService) { }



  //for enabling or disabling feature
  updateToggle(data: IUpdateToggle): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/api/BusinessFeatureFlag/feature/update`, data);
  }

  //for displaying business in dialog box 
  getBusinesses(apiEndpoint: string, featureId: number): Observable<IBusiness[]> {
    return this.http.get<IBusiness[]>(`${this.baseUrl}${apiEndpoint}?featureId=${featureId}`);
  }

  getLog(pageNumber: number, searchQuery: string): Observable<IPaginationLog> {
    const params = new HttpParams()
      .set('page', pageNumber)
      .set('pageSize', 12)
      .set('searchQuery', searchQuery !== null ? searchQuery : '')
    return this.http.get<IPaginationLog>(`${this.baseUrl}/api/Log`, { params })
  }

  //for feature(home) page
  getFeatures(selectedFilters2: IselectedFilters, pageNumber: number): Observable<IPaginatedFeatures> {
    const params = new HttpParams()
      .set('featureToggleType', selectedFilters2.featureFilter !== null ? selectedFilters2.featureFilter.toString() : '')
      .set('releaseToggleType', selectedFilters2.releaseFilter !== null ? selectedFilters2.releaseFilter.toString() : '')
      .set('isEnabled', selectedFilters2.enabledFilter !== null ? selectedFilters2.enabledFilter.toString() : '')
      .set('isDisabled', selectedFilters2.disabledFilter !== null ? selectedFilters2.disabledFilter.toString() : '')
      .set('pageNumber', pageNumber)
      .set('searchQuery', selectedFilters2.searchQuery !== null ? selectedFilters2.searchQuery : '');

    return this.http.get<IPaginatedFeatures>(`${this.baseUrl}/api/Filter`, { params });
  }

  downloadLogs() {
    return this.http.get(`${this.baseUrl}/api/Log/AllLogs`, {
      responseType: 'blob', // Expect a binary response
    });
  }
 
}
