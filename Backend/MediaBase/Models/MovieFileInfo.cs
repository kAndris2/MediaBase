using System.Text.RegularExpressions;
using MediaBase.Interfaces;

namespace MediaBase.Models
{
    public class MovieFileInfo : IMediaFileInfo, IMovie
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public string ParentFolder => Directory.GetParent(FilePath).Name;
        public string Extension => Path.GetExtension(FilePath);
        public bool IsStreamReady => Extension.Equals(".mp4");
        public string Title => GetTitle();
        public int Year => GetYear();

        public MovieFileInfo(string filePath)
        {
            FilePath = filePath;
        }

        public Movie Get()
        {
            return new Movie(Title, Year, IsStreamReady);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) 
                return false;

            var movieFileInfo = (MovieFileInfo)obj;

            return movieFileInfo.Title == Title && movieFileInfo.Year == Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Year);
        }

        private string GetTitle()
        {
            var match = Regex.Match(ParentFolder, @"^(?<title>.+?)\.\d{4}\.");

            if (match.Success)
            {
                return match.Groups["title"].Value.Replace('.', ' ');
            }

            return string.Empty;
        }

        private int GetYear()
        {
            var match = Regex.Match(ParentFolder, @"\.(\d{4})\.");

            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }

            return 0;
        }
    }
}