using MediaBase.Interfaces;
using MediaBase.Models;

namespace MediaBase.Services
{
    public abstract class PathProviderBase : IPathProvider
    {
        protected readonly MediaConfig _config;
        private readonly string[] _supportedExtensions;

        protected PathProviderBase(MediaConfig config, IDictionary<string, string> mimeTypes)
        {
            _config = config;
            _supportedExtensions = mimeTypes.Keys.ToArray();
        }

        abstract protected IEnumerable<IMediaFileInfo> MakeFileInfos(string[] filePaths);
        abstract protected bool IsFileRelevant(string filePath);

        public List<IMediaFileInfo> CollectMediaInfos()
        {
            var mediaInfos = new List<IMediaFileInfo>();

            foreach (var folder in _config.Folders)
            {
                if (!Directory.Exists(folder)) continue;

                var mInfos = CollectMediaInfosRecursive(folder)
                    .Where(x => IsFileExtensionRelevant(x.FilePath))
                    .Where(x => IsFileRelevant(x.FilePath));

                mediaInfos.AddRange(mInfos);
            }

            return mediaInfos;
        }

        private List<IMediaFileInfo> CollectMediaInfosRecursive(string path)
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
            var extension = Path.GetExtension(filePath)
                .Replace(".", "");

            return _supportedExtensions.Contains(extension);
        }
    }
}