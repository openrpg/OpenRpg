namespace OpenRpg.Core.Mapping
{
    public interface IMapper<in TIn, out TOut>
    {
        TOut Map(TIn persistedData);
    }
}