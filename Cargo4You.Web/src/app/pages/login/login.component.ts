import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SessionStorageService } from 'src/app/services/session-storage.service';
//import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UserService } from 'src/app/services/user.service';
import { LoginRequest } from 'src/clients/system-api/AdministrationApiClient.gen';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginRequestData: LoginRequest = new LoginRequest();
  @Input() error: string | null | undefined;

  @Output() submitEM = new EventEmitter();
  loginForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  ngOnInit(): void {
  }

  constructor(private userService: UserService,private sessionStorageService: SessionStorageService, 
                private _router: Router
              ) { }

 
 

  
  login() {
    if (!this.loginForm.valid) {
      return;
    }

    console.log(this.loginRequestData);


    this.userService.login(this.loginRequestData).subscribe(data => {
     console.log(data);
     if(data != null){
         this.sessionStorageService.setProfile(data);
        
         this._router.navigate(["/package-dimension"]);
     }
     else {
     //  this.snackBarService.callSnackBar(data);
    }
      });
    
     
  
  
}
signIn(){
  this._router.navigate(["/registration"]);
}
}
