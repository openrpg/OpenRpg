using OpenRpg.Core.Modifications;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Items.Templates;

namespace OpenRpg.UnitTests.Genres.Persistence.TestImplementations
{
    public class TestItemMapper : ItemMapper
    {
        private readonly DefaultItemModificationTemplate DefaultModification = new DefaultItemModificationTemplate();
        private readonly DefaultItemTemplate DefaultItemTemplate = new DefaultItemTemplate();
        
        public override IItemTemplate GetItemTemplateFor(int itemTemplateId)
        {
            return DefaultItemTemplate;
        }

        public override IItemModificationTemplate GetModificationsFor(int modificationId)
        {
            return DefaultModification;
        }
    }
}