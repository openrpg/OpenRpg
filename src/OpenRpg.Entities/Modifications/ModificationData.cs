using OpenRpg.Core.Templates;
using OpenRpg.Entities.Modifications.Variables;

namespace OpenRpg.Entities.Modifications
{
    public class ModificationData : ITemplateData<ModificationVariables>
    {
        public int TemplateId { get; }

        public ModificationVariables Variables { get; set; } = new ModificationVariables();
    }
}