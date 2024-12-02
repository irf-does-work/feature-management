import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { TOKEN_KEY } from '../shared/constants';
import { ILoginAccept, ILoginReturn, ISignUpAccept, ISignUpReturn } from '../interface/feature.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  public userId: number = 0;

  constructor(private router: Router, private http: HttpClient, private toastr: ToastrService) { }

  //for login
  login(data: ILoginAccept): Observable<ILoginReturn> {
    return this.http.post<ILoginReturn>(`${this.baseUrl}/api/Login`, data);
  }

  //for signup
  addUser(data: ISignUpAccept): Observable<ISignUpReturn> {
    return this.http.post<ISignUpReturn>(`${this.baseUrl}/api/User`, data);
  }

  isLoggedIn() {
    this.checkExpiry();
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
      return null;
    }
  }

  checkExpiry() {
    const payload = this.decodeToken();
    if (payload) {
      const expTime = payload.exp; // expiry in epoch seconds from payload
      const now = Date.now() / 1000; // Unix timestamp in milliseconds
      const diff = expTime - (Math.floor(now));

      if (diff <= 0) {
        this.deleteToken();
        this.router.navigate(['/user/login']);
        this.toastr.error('Please login again', 'Session Timeout')

      }
    }
  }


}
