using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MediaBase.Interfaces;
using MediaBase.Models;

namespace MediaBase.Services
{
    public class SeriesRequestManager : RequestManagerBase<SeriesEpisode>
    {
        public SeriesRequestManager(SeriesPathProvider pathProvider, IOptions<MediaConfigs> config)
            : base(pathProvider, config.Value.MimeTypes) { }

        public FileStreamResult GetStream(string title, int season, int episode)
        {
            var seriesFileInfos = pathProvider.CollectMediaInfos().Select(x => (SeriesFileInfo)x);
            var seriesFileInfo = seriesFileInfos.FirstOrDefault(x => x.Title == title && x.Season == season && x.Episode == episode);

            if (seriesFileInfo == null)
                throw new ArgumentException($"Series not found based on the specified params! - Title: {title} | Season: {season} | Episode: {episode}");

            return GetStream(seriesFileInfo.FilePath, seriesFileInfo.Extension);
        }

        protected override IEnumerable<SeriesEpisode> GetMedias(IEnumerable<IMediaFileInfo> mediaFileInfos)
        {
            return mediaFileInfos.Select(x => ((SeriesFileInfo)x).Get());
        }
    }
}