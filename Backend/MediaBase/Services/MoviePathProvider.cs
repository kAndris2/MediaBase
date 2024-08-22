using Microsoft.Extensions.Options;
using MediaBase.Interfaces;
using MediaBase.Models;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public class MoviePathProvider : PathProviderBase
    {
        public MoviePathProvider(ILogger<MoviePathProvider> logger, IOptions<MediaConfigs> mediaConfig, IOptions<ConversionConfig> conversionConfig)
            : base(logger, mediaConfig.Value.MovieConfig, conversionConfig.Value.StreamFolder) { }

        protected override bool IsFileRelevant(string filePath)
        {
            return !filePath.Contains("S0");
        }

        protected override IEnumerable<IMediaFileInfo> MakeFileInfos(string[] filePaths)
        {
            return filePaths.Select(filePath => new MovieFileInfo(filePath));
        }
    }
}