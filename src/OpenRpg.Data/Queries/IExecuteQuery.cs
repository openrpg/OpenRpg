namespace OpenRpg.Data.Queries
{
    /// <summary>
    /// Execute queries are for altering data in the repository such as mass updates or deletions
    /// </summary>
    public interface IExecuteQuery
    {
        object Execute(object dataSource);
    }
}