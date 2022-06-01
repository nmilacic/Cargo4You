import { HttpStatusCode } from "@angular/common/http";
import { Injectable, Input } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { AdministrationApiClient } from "src/clients/system-api/AdministrationApiClient";

@Injectable({
    providedIn: 'root'
})

export class SnackBarService {

    state: any


   
    constructor(public dialog: MatDialog,
        private snackBar: MatSnackBar,private administrationApiClient: AdministrationApiClient,private _router: Router
    ) { }


      showSnackbarAction(content: string, action: string | undefined) {
        let snackBar = this.snackBar.open(content, action,{
            duration: 10,
        });
     
        // snackBar.onAction().subscribe(() => {
        //     console.log("2");

        //   console.log("This will be called when snackbar button clicked");
        //   this._router.navigate(["/package-dimension"]);
        // });
        // snackBar.afterDismissed().subscribe(() => {
        //     console.log("1");
        //   console.log("This will be shown after snackbar disappeared");
        // });
        this.dismiss();
     
     // this.snackbarService.callSnackBar(data);
    }
    dismiss(){
        console.log("33");
        this.dialog.closeAll();
    }

}