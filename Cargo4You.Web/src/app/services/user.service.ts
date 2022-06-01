import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdministrationApiClient } from 'src/clients/system-api/AdministrationApiClient';
import { LoginData, LoginRequest, RegistrationData } from 'src/clients/system-api/AdministrationApiClient.gen';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private administrationApiClient: AdministrationApiClient) { }

userRegistration(registrationData : RegistrationData):Observable<boolean> {
  return this.administrationApiClient.userRegistration(registrationData)
}
login(loginRequest: LoginRequest):Observable<LoginData> {
  return this.administrationApiClient.login(loginRequest)
}
}
