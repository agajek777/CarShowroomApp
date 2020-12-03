import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class JWTTokenServiceService {
  jwtToken: string;
  decodedToken: { [key: string]: string };

  constructor() {
  }

  setToken(token: string) {
    if (token) {
      this.jwtToken = token;
      sessionStorage.setItem('access_token', token);
    }
  }

  removeToken() {
    this.jwtToken = '';
    sessionStorage.removeItem('access_token');
  }

  decodeToken() {
    if (sessionStorage.getItem('access_token')) {
    this.decodedToken = jwt_decode(sessionStorage.getItem('access_token'));
    }
  }

  getDecodeToken() {
    return jwt_decode(sessionStorage.getItem('access_token'));
  }

  getUser() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.displayname : null;
  }

  getEmailId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.email : null;
  }

  getExpiryTime() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.exp : null;
  }

  isTokenExpired(): boolean {
    const expiryTime: number = +this.getExpiryTime();
    if (expiryTime) {
      return ((1000 * expiryTime) - (new Date()).getTime()) < 5000;
    } else {
      return false;
    }
  }

  isUserLogged(): boolean {
    if (sessionStorage.getItem('access_token') === null) {
      return false;
    }

    this.decodeToken();

    return !this.isTokenExpired()
  }
}
