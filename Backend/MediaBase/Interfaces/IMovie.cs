namespace MediaBase.Interfaces
{
    public interface IMovie
    {
        string Title { get; }
        int Year { get; }
        bool IsStreamReady { get; }
    }
}