export class UserCredentials {
    public Username:string|null = null;
    public Password:string|null = null;

    public constructor(username:string|null, password:string|null){
        this.Username = username;
        this.Password = password;
    }
}
