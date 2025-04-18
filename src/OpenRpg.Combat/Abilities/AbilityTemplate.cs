using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Core.Templates;

namespace OpenRpg.Combat.Abilities
{
    /// <summary>
    /// A template for abilities that can be used by entities/whatever
    /// </summary>
    public class AbilityTemplate : ITemplate<AbilityTemplateVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public AbilityTemplateVariables Variables { get; set; } = new AbilityTemplateVariables();
    }
}