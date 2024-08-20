using Microsoft.AspNetCore.Mvc;
using MediaBase.Models;
using MediaBase.Interfaces;

namespace MediaBase.Services
{
    public class MovieRequestManager : RequestManagerBase<Movie>
    {
        public MovieRequestManager(MoviePathProvider pathProvider)
            : base(pathProvider) { }

        public FileStreamResult GetStream(string title, int year)
        {
            var movieFileInfos = pathProvider.CollectMediaInfos().Select(x => (MovieFileInfo)x);
            var movieFileInfo = movieFileInfos.FirstOrDefault(x => x.Title == title && x.Year == year);

            if (movieFileInfo == null)
                throw new ArgumentException($"Movie not found based on the specified params! - Title: {title} | Year: {year}");

            return GetStream(movieFileInfo.FilePath, movieFileInfo.Extension);
        }

        protected override IEnumerable<Movie> GetMedias(IEnumerable<IMediaFileInfo> mediaFileInfos)
        {
            return mediaFileInfos.Select(x => ((MovieFileInfo) x).Get());
        }
    }
}