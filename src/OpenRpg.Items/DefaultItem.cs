using OpenRpg.Items.Templates;

namespace OpenRpg.Items
{
    public class DefaultItem : IItem
    {
        public IItemTemplate Template { get; set; } = new DefaultItemTemplate();
        public IItemTemplateInstance Instance { get; set; } = new DefaultItemTemplateInstance();
    }
}