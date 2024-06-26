using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Requirements;

namespace OpenRpg.Genres.Fantasy.Requirements
{
    public class DefaultFantasyCharacterRequirementChecker : DefaultCharacterRequirementChecker
    {
        public override bool IsRequirementMet(ICharacter character, Requirement requirement)
        {
            if(requirement.RequirementType == FantasyRequirementTypes.StrengthRequirement)
            { return character.Stats.Strength() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.DexterityRequirement)
            { return character.Stats.Dexterity() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.IntelligenceRequirement)
            { return character.Stats.Intelligence() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.ConstitutionRequirement)
            { return character.Stats.Constitution() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.WisdomRequirement)
            { return character.Stats.Wisdom() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.CharismaRequirement)
            { return character.Stats.Charisma() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.MaxMagicRequirement)
            { return character.Stats.MaxMagic() >= requirement.AssociatedValue; }

            return base.IsRequirementMet(character, requirement);
        }
    }
}