using System;
using System.Collections.Generic;

namespace OpenRpg.Genres.Persistence.Items
{
    public class ItemData
    {
        public Guid Id { get; }
        public int ItemTemplateId { get; }
        public int[] ModificationTypes { get; }
        public IReadOnlyDictionary<int, object> Variables { get; }

        public ItemData(Guid id, int itemTemplateId, int[] modificationTypes, IReadOnlyDictionary<int, object> variables = null)
        {
            Id = id;
            ItemTemplateId = itemTemplateId;
            ModificationTypes = modificationTypes;
            Variables = variables ?? new Dictionary<int, object>();
        }
    }
}