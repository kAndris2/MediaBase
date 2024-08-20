using MediaBase.Interfaces;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public abstract class PathProviderBase : IPathProvider
    {
        protected readonly MediaConfig _config;
        protected readonly ILogger<IPathProvider> _logger;

        private readonly MediaConverter _converter;

        protected PathProviderBase(MediaConfig config, MediaConverter mediaConverter, ILogger<IPathProvider> logger)
        {
            _config = config;
            _converter = mediaConverter;
            _logger = logger;
        }

        abstract protected IEnumerable<IMediaFileInfo> MakeFileInfos(string[] filePaths);
        abstract protected bool IsFileRelevant(string filePath);

        public IEnumerable<IMediaFileInfo> CollectMediaInfos()
        {
            var mediaInfosFromStream = CollectMediaInfos(_config.StreamFolder).ToList();
            var mediaInfosFromSrc = CollectMediaInfos(_config.SourceFolders);
            var toConvert = mediaInfosFromSrc.Except(mediaInfosFromStream).ToList();
            var mediaInfos = new List<IMediaFileInfo>();

            foreach (var mediaFileInfo in mediaInfosFromSrc)
            {
                var mI = mediaInfosFromStream.FirstOrDefault(x => x.Equals(mediaFileInfo));

                mediaInfos.Add(mI != null ? mI : mediaFileInfo);
            }

            if (toConvert.Count() >= 1)
            {
                _ = _converter.Convert(toConvert, _config.StreamFolder);
            }

            return mediaInfos;
        }

        private IEnumerable<IMediaFileInfo> CollectMediaInfos(string[] folders)
        {
            var mediaInfos = new List<IMediaFileInfo>();

            foreach (var folder in folders)
            {
                var mInfos = CollectMediaInfos(folder);
                mediaInfos.AddRange(mInfos);
            }

            return mediaInfos;
        }

        private IEnumerable<IMediaFileInfo> CollectMediaInfos(string folder)
        {
            if (!Directory.Exists(folder))
            {
                _logger.LogWarning($"The specified folder does not exist! - {folder}");
                return Array.Empty<IMediaFileInfo>();
            }

            return CollectMediaInfosRecursive(folder)
                .Where(mI => IsFileExtensionRelevant(mI.FilePath))
                .Where(mI => IsFileRelevant(mI.FilePath));
        }

        private IEnumerable<IMediaFileInfo> CollectMediaInfosRecursive(string path)
        {
            var mediaInfos = new List<IMediaFileInfo>();
            var filePaths = Directory.GetFiles(path);

            mediaInfos.AddRange(
                MakeFileInfos(filePaths)
            );

            foreach (string directory in Directory.GetDirectories(path))
            {
                mediaInfos.AddRange(
                    CollectMediaInfosRecursive(directory)
                );
            }

            return mediaInfos;
        }

        private bool IsFileExtensionRelevant(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            return _config.SupportedExtensions.Contains(extension);
        }
    }
}