using OpenRpg.Core.Templates;
using OpenRpg.Entities.Modifications.Variables;

namespace OpenRpg.Entities.Modifications.Templates
{
    public class ModificationTemplate : ITemplate<ModificationTemplateVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }

        public int ModificationType { get; set; }
        public ModificationTemplateVariables Variables { get; set; } = new ModificationTemplateVariables();
    }
}