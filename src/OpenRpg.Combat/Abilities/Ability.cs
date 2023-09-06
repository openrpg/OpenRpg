using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.Combat.Abilities
{
    /// <summary>
    /// An ability wraps up the notion of a special ability that can be used for combat purposes
    /// </summary>
    public class Ability : IHasDataId, IHasLocaleDescription
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public IAbilityVariables AbilityVariables { get; set; } = new DefaultAbilityVariables();
    }
}