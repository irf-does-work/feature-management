import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { ILoginAccept, IselectedFilters, ISignUpAccept} from './interface/feature.interface';


@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private baseUrl = environment.apiUrl;
  public userId: number = 0;

  constructor(private http: HttpClient) {
    
  }
  
  login(data: ILoginAccept): Observable<object> {
    return this.http.post(`${this.baseUrl}/api/AddUser/ValidateUser`, data)
  }

  addUser(data: ISignUpAccept): Observable<object> {
    return this.http.post(`${this.baseUrl}/api/AddUser`, data);
  }

  getFeatures(selectedFilters2: IselectedFilters): Observable<any> {
    const params = new HttpParams()
      .set('featureToggleType', selectedFilters2.featureFilter !== null ? selectedFilters2.featureFilter.toString() : '')
      .set('releaseToggleType', selectedFilters2.releaseFilter !== null ? selectedFilters2.releaseFilter.toString() : '')
      .set('isEnabled', selectedFilters2.enabledFilter !== null ? selectedFilters2.enabledFilter.toString() : '')
      .set('isDisabled', selectedFilters2.disabledFilter !== null ? selectedFilters2.disabledFilter.toString() : '');

    return this.http.get(`${this.baseUrl}/api/Filter`,{params});
  }

}
