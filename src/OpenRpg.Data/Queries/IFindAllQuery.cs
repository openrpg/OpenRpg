using System.Collections.Generic;

namespace OpenRpg.Data.Queries
{
    /// <summary>
    /// The FindAll Query is meant to represent a way to get a list of results back from the repository
    /// </summary>
    /// <typeparam name="T">The data type to return</typeparam>
    public interface IFindAllQuery<out T> : IFindQuery<IEnumerable<T>>
    { }
}