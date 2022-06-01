import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
//import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UserService } from 'src/app/services/user.service';
import { RegistrationData } from 'src/clients/system-api/AdministrationApiClient.gen';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registrationData: RegistrationData = new RegistrationData();
  @Input() error: string | null | undefined;
action: boolean = true;
 
  signInForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

 

  constructor(private userService: UserService, private _router: Router,private snackBar: MatSnackBar, private snackBarService: SnackBarService
              ) { }

 

  ngOnInit(): void {
  }
  signIn(){
    if(this.signInForm.valid){
    this.userService.userRegistration(this.registrationData).subscribe((data)=>{
     if(data == true)
      {
      this.snackBarService.showSnackbarAction('Registration successful!','Close' );
      this._router.navigate(["/package-dimension"]);
    
      }
      else
       {
        this.snackBarService.showSnackbarAction('Registration faild!Please try again.','Close');
      }

    });
  }

  }

}


