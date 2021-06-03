namespace OpenRpg.Data.Queries
{
    /// <summary>
    /// The find query represents a query that finds something specific from the database, it a specific
    /// </summary>
    /// <typeparam name="T">Specific Type to return</typeparam>
    /// <remarks>This is meant as a way to return types different to the repository type</remarks>
    public interface IFindQuery<out T>
    {
        T Execute(object dataSource);
    }
}