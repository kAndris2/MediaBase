namespace MediaBase.Interfaces
{
    public interface IMediaFileInfo
    {
        string FilePath { get; set; }
        string FileName { get; }
        string ParentFolder { get; }
        string Extension { get; }
    }
}