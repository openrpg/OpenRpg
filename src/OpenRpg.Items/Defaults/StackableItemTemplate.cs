using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Defaults
{
    public class StackableItemTemplate : ItemTemplate, IStackableItemTemplate
    {
        public int StackableAmount { get; set; }
    }
}