import { SeriesEpisode } from "../../Models/SeriesEpisode";
import { BackendServiceBase } from "./BackendServiceBase";

export class SeriesBackendService extends BackendServiceBase<SeriesEpisode> {
    constructor() {
        super(SeriesEpisode);
    }

    protected getConstructorName(): string {
        return this.constructor.name;
    }
}