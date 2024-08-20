import { Movie } from "../../Models/Movie";
import { BackendServiceBase } from "./BackendServiceBase";

export class MovieBackendService extends BackendServiceBase<Movie> {
    constructor() {
        super(Movie);
    }
    
    protected getConstructorName(): string {
        return this.constructor.name;
    }
}