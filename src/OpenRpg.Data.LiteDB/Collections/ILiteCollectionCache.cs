using System.Collections;

namespace OpenRpg.Data.LiteDB.Collections;

public interface ILiteCollectionCache
{
    IEnumerable GetAll();
    object? Get(object id);
    void Create(object data, object id);
    void Update(object data, object id);
    bool Delete(object id);
    bool Exists(object id);
}