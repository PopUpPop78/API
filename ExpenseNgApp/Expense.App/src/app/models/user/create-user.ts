import { LoginUser } from "./login-user";

export class CreateUser implements LoginUser {
    firstName!: string;
    lastName!: string;
    email!: string;
    password!: string;

}
