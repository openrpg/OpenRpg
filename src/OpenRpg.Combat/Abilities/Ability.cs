using OpenRpg.Core.Common;

namespace OpenRpg.Combat.Abilities
{
    public class Ability : IHasDataId, IHasLocaleDescription
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IAbilityVariables AbilityVariables { get; set; } = new DefaultAbilityVariables();
    }
}