using Microsoft.Extensions.Options;
using MediaBase.Models;
using MediaBase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediaBase.Services
{
    public class MovieRequestManager : RequestManagerBase<Movie>
    {
        public MovieRequestManager(MoviePathProvider pathProvider, IOptions<MediaConfigs> config)
            : base(pathProvider, config.Value.MimeTypes) { }

        public FileStreamResult GetStream(string title)
        {
            var mediaFileInfos = pathProvider.CollectMediaInfos();
            var mediaFileInfo = mediaFileInfos.FirstOrDefault(x => ((MovieFileInfo) x).Title == title);

            if (mediaFileInfo == null)
                throw new ArgumentException($"Movie not found based on the specified title! - {title}");

            return GetStream(mediaFileInfo.FilePath, mediaFileInfo.Extension);
        }

        protected override IEnumerable<Movie> GetMedias(IList<IMediaFileInfo> mediaFileInfos)
        {
            return mediaFileInfos.Select(x => ((MovieFileInfo) x).Get());
        }
    }
}