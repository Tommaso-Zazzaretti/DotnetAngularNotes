export class UserProfile{
    name:string|null = null;
    surname:string|null = null;
    birthDate:string|null = null;
    username:string|null = null;

    constructor(Name:string|null,Surname:string|null,BirthDate:string|null,UserName:string|null){
        this.name = Name;
        this.surname = Surname;
        this.birthDate = BirthDate;
        this.username = UserName;
    }
}
