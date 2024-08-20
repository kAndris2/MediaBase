using Microsoft.Extensions.Options;
using MediaBase.Interfaces;
using MediaBase.Models;

namespace MediaBase.Services
{
    public class MoviePathProvider : PathProviderBase
    {
        public MoviePathProvider(IOptions<MediaConfigs> config)
            : base(config.Value.MovieConfig) { }

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