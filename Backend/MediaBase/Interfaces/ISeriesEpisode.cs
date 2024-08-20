namespace MediaBase.Interfaces
{
    public interface ISeriesEpisode
    {
        string Title { get; }
        int Season { get; }
        int Episode { get; }
        bool IsStreamReady { get; }
    }
}