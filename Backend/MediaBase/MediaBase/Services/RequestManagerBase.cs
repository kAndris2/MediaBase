using Microsoft.AspNetCore.Mvc;
using MediaBase.Interfaces;

namespace MediaBase.Services
{
    public abstract class RequestManagerBase<T> : IRequestManager<T>
    {
        protected readonly IPathProvider pathProvider;

        protected RequestManagerBase(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        abstract protected IEnumerable<T> GetMedias(IEnumerable<IMediaFileInfo> mediaFileInfos);

        public IEnumerable<T> GetTitles()
        {
            var mediaFileInfos = pathProvider.CollectMediaInfos();

            return GetMedias(mediaFileInfos);
        }

        protected FileStreamResult GetStream(string filePath, string extension)
        {
            /*
            if (!_mimeTypes.ContainsKey(extension))
                throw new InvalidOperationException($"Unsupported extension! - {extension}");
            */

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            
            return new FileStreamResult(fileStream, "video/mp4")
            {
                EnableRangeProcessing = true
            };
        }
    }
}