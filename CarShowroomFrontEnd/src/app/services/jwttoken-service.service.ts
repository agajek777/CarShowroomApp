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
        localStorage.setItem('access_token', token);
      }
    }

    decodeToken() {
      if (this.jwtToken) {
      this.decodedToken = jwt_decode(this.jwtToken);
      }
    }

    getDecodeToken() {
      return jwt_decode(this.jwtToken);
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
}
