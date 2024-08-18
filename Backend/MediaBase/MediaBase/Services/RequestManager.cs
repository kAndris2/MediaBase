using Microsoft.Extensions.Options;
using MediaBase.Models;

namespace MediaBase.Services
{
    public class RequestManager
    {
        private readonly MoviePathProvider _pathProvider;
        private readonly IDictionary<string, string> _mimeTypes;

        public RequestManager(MoviePathProvider pathProvider, IOptions<MediaConfigs> config)
        {
            _pathProvider = pathProvider;
            _mimeTypes = config.Value.MimeTypes;
        }

        public IEnumerable<Movie> GetMovies()
        {
            var mInfos = _pathProvider.CollectMediaInfos();
            var movies = mInfos.Select(x => ((MovieFileInfo)x).Get());

            return movies;
        }
    }
}