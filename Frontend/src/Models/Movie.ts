export class Movie {
    public readonly title: string;
    public readonly year: number;

    constructor(movie: Movie) {
        Object.assign(this, movie);
    }
}