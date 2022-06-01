import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Client } from "./AdministrationApiClient.gen";

@Injectable({ providedIn: 'root' })
export class AdministrationApiClient extends Client {
    [x: string]: any;
  
    constructor(private httpClient : HttpClient) {
    super(httpClient, environment.apiUrl);
    }
}
