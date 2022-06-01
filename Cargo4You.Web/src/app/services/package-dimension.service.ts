import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdministrationApiClient } from 'src/clients/system-api/AdministrationApiClient';
import { CourierOfferPrice, PackageDetailData } from 'src/clients/system-api/AdministrationApiClient.gen';

@Injectable({
  providedIn: 'root'
})
export class PackageDimensionService {

  constructor(private administrationApiClient: AdministrationApiClient) { }

  getPrice(packageDetail: PackageDetailData): Observable<CourierOfferPrice> {
    return this.administrationApiClient.getPrice(packageDetail);
  }

}
