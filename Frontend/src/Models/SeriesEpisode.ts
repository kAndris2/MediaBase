export class SeriesEpisode {
    public readonly title: string;
    public readonly season: number;
    public readonly episode: number;

    constructor(seriesEpisode: SeriesEpisode) {
        Object.assign(this, seriesEpisode);
    }
}