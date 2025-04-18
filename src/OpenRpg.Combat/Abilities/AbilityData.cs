using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Core.Templates;

namespace OpenRpg.Combat.Abilities
{
    /// <summary>
    /// Data related to instances of abilities
    /// </summary>
    public class AbilityData : ITemplateData<AbilityVariables>
    {
        public int TemplateId { get; set; }
        
        public AbilityVariables Variables { get; set; } = new AbilityVariables();
    }
}