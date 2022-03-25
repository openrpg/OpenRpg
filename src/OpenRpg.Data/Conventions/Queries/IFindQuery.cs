using System.Collections.Generic;

namespace OpenRpg.Data.Conventions.Queries
{
    /// <summary>
    /// The find query represents a query that finds something specific from the data source, it a specific
    /// </summary>
    /// <typeparam name="T">Specific Type to return</typeparam>
    /// <remarks>This is meant as a way to return types different to the repository type</remarks>
    public interface IFindQuery<T> : IQuery<IEnumerable<T>>
    {
    }
}