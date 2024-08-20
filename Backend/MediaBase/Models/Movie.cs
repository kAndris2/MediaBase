using MediaBase.Interfaces;

namespace MediaBase.Models
{
    public class Movie : IMovie
    {
        public string Title => _title;
        public int Year => _year;
        public bool IsStreamReady => _isStreamReady;

        private readonly string _title;
        private readonly int _year;
        private readonly bool _isStreamReady;

        public Movie(string title, int year, bool isStreamReady)
        {
            _title = title;
            _year = year;
            _isStreamReady = isStreamReady;
        }
    }
}