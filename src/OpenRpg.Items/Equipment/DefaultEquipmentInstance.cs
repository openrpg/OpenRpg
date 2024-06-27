using System.Collections.Generic;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Equipment
{
    public class DefaultEquipmentInstance : IEquipmentInstance
    {
        public Dictionary<int, IItemTemplateInstance> Equipment { get; set; } = new Dictionary<int, IItemTemplateInstance>();
        public IEquipmentVariables Variables { get; set; } = new DefaultEquipmentVariables();
    }
}