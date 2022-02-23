namespace OpenRpg.Data
{
    public interface IQuery<out T>
    {
        T Execute(IDataSource dataSource);
    }
}