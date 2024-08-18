import { MovieBackendService } from "../Services/Http/MovieBackendService";
import { SeriesBackendService } from "../Services/Http/SeriesBackendService";

export interface IServiceContext {
    movieBackendService: MovieBackendService;
    seriesBackendService: SeriesBackendService;
}