import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { IBusiness, Ilog, ILoginAccept, ILoginReturn, IPaginationLog, IselectedFilters, ISignUpAccept, IUpdateToggle } from './interface/feature.interface';
import { TOKEN_KEY } from './shared/constants';


@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private baseUrl = environment.apiUrl;
  public userId: number = 0;
  
  constructor(private http: HttpClient) { }

  //for login
  login(data: ILoginAccept): Observable<ILoginReturn> {
    return this.http.post<ILoginReturn>(`${this.baseUrl}/api/Login`, data);
  }

  //for signup
  addUser(data: ISignUpAccept): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/User`, data);
  }


  //for enabling or disabling feature
  updateToggle(data: IUpdateToggle): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/BusinessFeatureFlag/feature/update`, data);
  }

  //for displaying business in dialog box 
  getBusinesses(apiEndpoint: string, featureId: number): Observable<IBusiness[]> {
    return this.http.get<IBusiness[]>(`${this.baseUrl}${apiEndpoint}?featureId=${featureId}`);
  }

  getLog(pageNumber: number, searchQuery:string) : Observable<IPaginationLog> {
    const params = new HttpParams()
    .set('page',pageNumber)
    .set('pageSize',12)
    .set('searchQuery',searchQuery !== null ? searchQuery : '')
    return this.http.get<IPaginationLog>(`${this.baseUrl}/api/Log`,{params})
  }
  


  //auth service

  isLoggedIn() {
    return localStorage.getItem(TOKEN_KEY) != null ? true : false;
  }

  saveToken(token: string) {
    localStorage.setItem(TOKEN_KEY, token);
  }

  deleteToken() {
    localStorage.removeItem(TOKEN_KEY);
  }

  decodeToken() {
    try {
      const token = localStorage.getItem(TOKEN_KEY);
      if (!token) {
        console.error("Token is undefined or empty.");
        return null;
      }

      const tokenParts = token.split('.');
      if (tokenParts.length !== 3) {
        return null;
      }

      const payloadBase64 = tokenParts[1];
      const payloadJson = JSON.parse(window.atob(payloadBase64));
      return payloadJson;
    } catch (error) {
      console.error("Failed to decode token:", error);
      return null;
    }
  }


  //for feature(home) page
  getFeatures(selectedFilters2: IselectedFilters, pageNumber: number): Observable<any> {
    const params = new HttpParams()
      .set('featureToggleType', selectedFilters2.featureFilter !== null ? selectedFilters2.featureFilter.toString() : '')
      .set('releaseToggleType', selectedFilters2.releaseFilter !== null ? selectedFilters2.releaseFilter.toString() : '')
      .set('isEnabled', selectedFilters2.enabledFilter !== null ? selectedFilters2.enabledFilter.toString() : '')
      .set('isDisabled', selectedFilters2.disabledFilter !== null ? selectedFilters2.disabledFilter.toString() : '')
      .set('pageNumber', pageNumber)
      .set('searchQuery', selectedFilters2.searchQuery !== null ? selectedFilters2.searchQuery : '');

    return this.http.get(`${this.baseUrl}/api/Filter`, { params });
  }

  downloadLogs(){
    return this.http.get(`${this.baseUrl}/api/Log/AllLogs`, {
      responseType: 'blob', // Expect a binary response
    });
  }

}
