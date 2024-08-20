using Microsoft.Extensions.Options;
using MediaBase.Interfaces;
using MediaBase.Models;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public class SeriesPathProvider : PathProviderBase
    {
        public SeriesPathProvider(IOptions<MediaConfigs> config, MediaConverter mediaConverter, ILogger<SeriesPathProvider> logger)
            : base(config.Value.MovieConfig, mediaConverter, logger) { }

        protected override bool IsFileRelevant(string filePath)
        {
            return filePath.Contains("S0");
        }

        protected override IEnumerable<IMediaFileInfo> MakeFileInfos(string[] filePaths)
        {
            return filePaths.Select(filePath => new SeriesFileInfo(filePath));
        }
    }
}