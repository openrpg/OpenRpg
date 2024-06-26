using System.Collections.Generic;

namespace OpenRpg.Genres.Persistence.Items.Equipment
{
    public class EquipmentData
    {
        public IReadOnlyDictionary<int, ItemData> Slots { get; }
        public IReadOnlyDictionary<int, object> Variables { get; }

        public EquipmentData(IReadOnlyDictionary<int, ItemData> slots, IReadOnlyDictionary<int, object> variables = null)
        {
            Slots = slots;
            Variables = variables ?? new Dictionary<int, object>();
        }
    }
}