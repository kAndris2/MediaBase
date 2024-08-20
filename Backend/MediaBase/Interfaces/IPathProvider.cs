namespace MediaBase.Interfaces
{
    public interface IPathProvider
    {
        IEnumerable<IMediaFileInfo> CollectMediaInfos();
    }
}