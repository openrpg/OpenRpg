using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Equipment
{
    public interface IEquipmentInstance : IHasVariables<IEquipmentVariables>
    {
        Dictionary<int, IItemTemplateInstance> Equipment { get; set; }
    }
}