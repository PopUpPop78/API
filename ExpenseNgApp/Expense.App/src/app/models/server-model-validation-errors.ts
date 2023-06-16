import { HttpErrorResponse } from "@angular/common/http";

export class ServerModelValidationErrors{

    serverValidationErrors: ServerModelValidationError[] = [];

    constructor(response: HttpErrorResponse){
        const err = response.error;
        Object.keys(err).forEach(prop => {
            Object.keys(err[prop]).forEach(error => {
                
                let serverError = new ServerModelValidationError(prop, err[prop]);
                this.serverValidationErrors.push(serverError);
            })
          })
    }
}

export class ServerModelValidationError{
    key!: string;
    error!: string[];

    constructor(key: string, error: string[]){
        this.key = key.toLowerCase();
        this.error = error;
    }
}