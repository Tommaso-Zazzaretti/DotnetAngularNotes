import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { UserCredentials } from './../../Models/UserCredentials';
import { RegexMatchValidator } from './../../Validators/CustomValidators';
import { AuthenticationService } from './../../Services/authentication.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

    public LOGIN_CREDENTIALS: UserCredentials; //Model bind variable
    public LOGIN_FORM: FormGroup;
    public SUBMITTED: boolean = false;
    private REG_EXP_USERNAME: RegExp = /.*[0-9].*/;

    constructor(private formBuilder: FormBuilder,private service: AuthenticationService) {
        //Model init
        this.LOGIN_CREDENTIALS = new UserCredentials(null,null);
        //Form Init
        this.LOGIN_FORM = this.formBuilder.group(
            {
                usernameControl:        ['', [Validators.required,Validators.minLength(3),Validators.maxLength(10)] ],
                passwordControl:        ['', [Validators.required,Validators.minLength(3),Validators.maxLength(10)] ],
            },
            {
                validators: [RegexMatchValidator('usernameControl',this.REG_EXP_USERNAME)]
            });
        //Data Binding
        this.LOGIN_FORM.get('usernameControl')!.valueChanges.subscribe(value => this.LOGIN_CREDENTIALS.Username = value);
        this.LOGIN_FORM.get('passwordControl')!.valueChanges.subscribe(value => this.LOGIN_CREDENTIALS.Password = value);
    }

    public onReset():  void {
        this.SUBMITTED = false;
        this.LOGIN_FORM.reset();
    }

    public onSubmit(): void {
        this.SUBMITTED = true;
        if(this.LOGIN_FORM.invalid){ return; }
        //Action on correct form compilation
        this.service.Login(this.LOGIN_CREDENTIALS);
    }

    ngOnInit(): void { }
}
