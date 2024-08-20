namespace MediaBase.Interfaces
{
    public interface IRequestManager<T>
    {
        IEnumerable<T> GetTitles();
    }
}