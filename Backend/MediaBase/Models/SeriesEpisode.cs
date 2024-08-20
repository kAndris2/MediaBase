using MediaBase.Interfaces;

namespace MediaBase.Models
{
    public class SeriesEpisode : ISeriesEpisode
    {
        public string Title => _title;
        public int Season => _season;
        public int Episode => _episode;
        public bool IsStreamReady => _isStreamReady;

        private readonly string _title;
        private readonly int _season;
        private readonly int _episode;
        private readonly bool _isStreamReady;

        public SeriesEpisode(string title, int season, int episode, bool isStreamReady)
        {
            _title = title;
            _season = season;
            _episode = episode;
            _isStreamReady = isStreamReady;
        }
    }
}