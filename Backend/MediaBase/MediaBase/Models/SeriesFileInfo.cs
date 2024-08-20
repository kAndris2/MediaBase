using System.Text.RegularExpressions;
using MediaBase.Interfaces;

namespace MediaBase.Models
{
    public class SeriesFileInfo : IMediaFileInfo, ISeriesEpisode
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public string ParentFolder => Directory.GetParent(FilePath).Name;
        public string Extension => Path.GetExtension(FilePath);
        public bool IsStreamReady => Extension.Equals(".mp4");
        public string Title => GetTitleOfSeries();
        public int Season { get; set; }
        public int Episode { get; set; }

        public SeriesFileInfo(string filePath)
        {
            FilePath = filePath;
            GuessSeasonAndEpisode();
        }

        public SeriesEpisode Get()
        {
            return new SeriesEpisode(Title, Season, Episode, IsStreamReady);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var seriesFileInfo = (SeriesFileInfo)obj;

            return seriesFileInfo.Title == Title && seriesFileInfo.Season == Season && seriesFileInfo.Episode == Episode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Season, Episode);
        }

        private void GuessSeasonAndEpisode()
        {
            var match = Regex.Match(FileName, @"S(?<season>\d{1,2})E(?<episode>\d{1,2})");

            if (match.Success)
            {
                Season = int.Parse(match.Groups["season"].Value);
                Episode = int.Parse(match.Groups["episode"].Value);
            }
        }

        private string GetTitleOfSeries()
        {
            var match = Regex.Match(ParentFolder, @"^(.*?)(?=\.S\d{2})");

            if (match.Success)
            {
                return match.Value.Replace('.', ' ');
            }

            return string.Empty;
        }
    }
}