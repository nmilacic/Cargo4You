import { Injectable } from '@angular/core';
import { LoginData } from 'src/clients/system-api/AdministrationApiClient.gen';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {

  constructor() { }

  setProfile(loginData: LoginData) {
    sessionStorage.setItem("profile", JSON.stringify(loginData));
}

getProfile() : LoginData {
    return JSON.parse(sessionStorage.getItem("profile")!);
}
}
