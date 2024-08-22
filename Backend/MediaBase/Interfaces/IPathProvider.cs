namespace MediaBase.Interfaces
{
    public interface IPathProvider
    {
        IEnumerable<IMediaFileInfo> CollectMediaInfos();
        IEnumerable<IMediaFileInfo> GetMediasToConvert();
    }
}