import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CourierOfferPrice } from 'src/clients/system-api/AdministrationApiClient.gen';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ResultComponent>,public dialog: MatDialog,@Inject(MAT_DIALOG_DATA) public data: CourierOfferPrice) { }

  ngOnInit(): void {
    console.log(this.data);
    console.log(this.data.courier?.name);
  }
  closeDialog(): void  {
    console.log("nev");
    this.dialogRef.close();
  }
}
