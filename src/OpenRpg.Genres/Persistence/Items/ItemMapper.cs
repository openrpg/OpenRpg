using System.Linq;
using OpenRpg.Core.Modifications;
using OpenRpg.Items;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Genres.Persistence.Items
{
    public abstract class ItemMapper : IItemMapper
    {
        public IItem Map(ItemData data)
        {
            var itemVariables = new DefaultItemVariables(data.Variables
                .ToDictionary(x => x.Key, x => x.Value));

            return new DefaultItem
            {
                UniqueId = data.Id,
                Variables = itemVariables,
                Template = GetItemTemplateFor(data.ItemTemplateId),
                Modifications = data.ModificationTypes.Select(GetModificationsFor)
            };
        }

        public abstract IItemTemplate GetItemTemplateFor(int itemTemplateId);
        public abstract IItemModificationTemplate GetModificationsFor(int modificationId);
    }
}