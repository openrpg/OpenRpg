using OpenRpg.Core.Common;

namespace OpenRpg.Data.Defaults
{
    public abstract class InMemoryFindDataQuery<T> : InMemoryFindQuery<T> where T : IHasDataId
    { }
}