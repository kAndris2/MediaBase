using MediaBase.Interfaces;

namespace MediaBase.Models
{
    public class Movie : IMovie
    {
        public string Title => _title;
        public int Year => _year;

        private readonly string _title;
        private readonly int _year;

        public Movie(string title, int year)
        {
            _title = title;
            _year = year;
        }
    }
}