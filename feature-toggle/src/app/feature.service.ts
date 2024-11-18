import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { ILoginAccept, ISignUpAccept} from './interface/feature.interface';


@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private baseUrl = environment.apiUrl;
  public userId: number = 0;

  constructor(private http: HttpClient) {
    // Retrieve userId from sessionStorage on service initialization
    // const storedUserId = sessionStorage.getItem('userId');
    // const storedUserId = 0; 
    // if (storedUserId) {
    //   this.userId = parseInt(storedUserId, 10); // Parse the stored string value to a number
    // }
  }



  
  login(data: ILoginAccept): Observable<object> {
    return this.http.post(`${this.baseUrl}/api/AddUser/ValidateUser`, data)
    // .pipe(
    //   tap((response: any) => {
    //     if (response) {
    //       this.userId = response;
    //       // sessionStorage.setItem('userId', this.userId.toString()); // Store userId in sessionStorage
    //       console.log('UserId stored in sessionStorage:', this.userId);
    //     } else {
    //       console.error('Invalid login response or missing userId');
    //     }
    //   })
    // );
  }

  addUser(data: ISignUpAccept): Observable<object> {
    return this.http.post(`${this.baseUrl}/api/AddUser`, data);
  }
}
