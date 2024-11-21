import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { IBusiness, ILoginAccept, ISignUpAccept, IUpdateToggle} from './interface/feature.interface';
import { TOKEN_KEY } from './shared/constants';


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


  
  login(data: ILoginAccept): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/Login`, data);
  }

  addUser(data: ISignUpAccept): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/User`, data);
  }

  updateToggle(data: IUpdateToggle) : Observable<any> {
    return this.http.post(`${this.baseUrl}/api/BusinessFeatureFlag/feature/update`,data);
  }

  getBusinesses(apiEndpoint: string, featureId: number): Observable<IBusiness[]> {
    return this.http.get<IBusiness[]>(`${this.baseUrl}${apiEndpoint}?featureId=${featureId}`);
  }
  





  //auth

  isLoggedIn(){
    return localStorage.getItem(TOKEN_KEY)!= null? true : false;
  }

  saveToken(token: string){
    localStorage.setItem(TOKEN_KEY, token);
  }
  
  deleteToken(){
    localStorage.removeItem(TOKEN_KEY);
  }

  // decode(){
  //   const aa = JSON.parse(window.atob(TOKEN_KEY));
  //   console.log("hi" + aa);
  //   return aa;
  // }

  decodeToken() {
    try {
      const token = localStorage.getItem(TOKEN_KEY); 
      if (!token) {
        console.error("Token is undefined or empty.");
        return null;
      }
  
      const tokenParts = token.split('.');
      if (tokenParts.length !== 3) {
        console.error("Invalid JWT format.");
        return null;
      }
  
      const payloadBase64 = tokenParts[1];
      const payloadJson = JSON.parse(window.atob(payloadBase64));
      console.log("Decoded payload:", payloadJson);
      return payloadJson;
    } catch (error) {
      console.error("Failed to decode token:", error);
      return null;
    }
  }
  
}
