using MediaBase.Interfaces;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public abstract class PathProviderBase : IPathProvider
    {
        protected readonly ILogger<IPathProvider> _logger;
        protected readonly MediaConfig _config;

        private readonly string _streamFolderPath;

        protected PathProviderBase(ILogger<IPathProvider> logger, MediaConfig config, string streamFolderPath)
        {
            _config = config;
            _logger = logger;
            _streamFolderPath = streamFolderPath;
        }

        abstract protected IEnumerable<IMediaFileInfo> MakeFileInfos(string[] filePaths);
        abstract protected bool IsFileRelevant(string filePath);

        public IEnumerable<IMediaFileInfo> GetMediasToConvert()
        {
            if (!CheckConfiguration()) return new List<IMediaFileInfo>();

            var mediaInfosFromStream = CollectMediaInfos(_streamFolderPath);
            var mediaInfosFromSrc = CollectMediaInfos(_config.SourceFolders);

            return mediaInfosFromSrc.Except(mediaInfosFromStream);
        }

        public IEnumerable<IMediaFileInfo> CollectMediaInfos()
        {
            if (!CheckConfiguration()) return new List<IMediaFileInfo>();

            var mediaInfosFromStream = CollectMediaInfos(_streamFolderPath);
            var mediaInfosFromSrc = CollectMediaInfos(_config.SourceFolders);
            var mediaInfos = new List<IMediaFileInfo>();

            foreach (var mediaFileInfo in mediaInfosFromSrc)
            {
                var mI = mediaInfosFromStream.FirstOrDefault(x => x.Equals(mediaFileInfo));

                mediaInfos.Add(mI != null ? mI : mediaFileInfo);
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

        private bool CheckConfiguration()
        {
            var errorMessage = string.Empty;
            var faultyField = string.Empty;

            if (string.IsNullOrEmpty(_streamFolderPath))
                faultyField = "StreamFolder";

            else if (_config.SourceFolders == null)
                faultyField = "SourceFolders";

            else if (_config.SupportedExtensions == null)
                faultyField = "SupportedExtensions";

            if (!string.IsNullOrEmpty(faultyField))
            {
                errorMessage = $"The '{faultyField}' cannot be empty! Please check the configuration!";
                _logger.LogError(errorMessage);
            }

            return string.IsNullOrEmpty(errorMessage);
        }
    }
}