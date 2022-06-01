import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { PackageDimensionService } from 'src/app/services/package-dimension.service';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { CourierOfferPrice, PackageDetailData } from 'src/clients/system-api/AdministrationApiClient.gen';
import { ResultComponent } from '../result/result.component';

@Component({
  selector: 'app-package-dimension',
  templateUrl: './package-dimension.component.html',
  styleUrls: ['./package-dimension.component.scss']
})
export class PackageDimensionComponent implements OnInit {
  @Input() error: string | null | undefined;

  @Output() submitEM = new EventEmitter();
  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  packageDimension: PackageDetailData = new PackageDetailData();
  priceDetail: CourierOfferPrice | undefined;
  constructor(private packageDimesnionService: PackageDimensionService,public dialog: MatDialog,private sessionStorageService: SessionStorageService) { }

  ngOnInit(): void {
    var tt=this.sessionStorageService.getProfile();
    console.log(tt);
    
  }

  getPrice(){
    console.log(this.packageDimension);
    
    this.packageDimesnionService.getPrice(this.packageDimension).subscribe((data)=> {
        this.priceDetail=data;
        console.log(this.priceDetail);
        console.log(this.priceDetail.price);
        console.log(this.priceDetail.courier?.name);

        this.dialog.closeAll();
        const dialogRef = this.dialog.open(ResultComponent, {
          data: this.priceDetail,
          width: "500"
        });
        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          
        });
        
        
    });
    
    
  

  }

}
