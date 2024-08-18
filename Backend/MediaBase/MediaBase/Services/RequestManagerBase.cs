using Microsoft.AspNetCore.Mvc;
using MediaBase.Interfaces;

namespace MediaBase.Services
{
    public abstract class RequestManagerBase<T> : IRequestManager<T>
    {
        protected readonly IPathProvider pathProvider;
        private readonly IDictionary<string, string> _mimeTypes;

        protected RequestManagerBase(IPathProvider pathProvider, IDictionary<string, string> mimeTypes)
        {
            this.pathProvider = pathProvider;
            _mimeTypes = mimeTypes;
        }

        abstract protected IEnumerable<T> GetMedias(IList<IMediaFileInfo> mediaFileInfos);

        public IEnumerable<T> GetTitles()
        {
            var mediaFileInfos = pathProvider.CollectMediaInfos();

            return GetMedias(mediaFileInfos);
        }

        protected FileStreamResult GetStream(string filePath, string extension)
        {
            if (!_mimeTypes.ContainsKey(extension))
                throw new InvalidOperationException($"Unsupported extension! - {extension}");

            var mimeType = $"video/{_mimeTypes[extension]}";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            
            return new FileStreamResult(fileStream, mimeType)
            {
                EnableRangeProcessing = true
            };
        }
    }
}