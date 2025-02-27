using OpenRpg.Entities.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Requirements;

namespace OpenRpg.Genres.Fantasy.Requirements
{
    public class DefaultFantasyCharacterRequirementChecker : DefaultCharacterRequirementChecker
    {
        public override bool IsRequirementMet(Character character, Requirement requirement)
        {
            if(requirement.RequirementType == FantasyRequirementTypes.StrengthRequirement)
            { return character.Stats.Strength() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.DexterityRequirement)
            { return character.Stats.Dexterity() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.IntelligenceRequirement)
            { return character.Stats.Intelligence() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.ConstitutionRequirement)
            { return character.Stats.Constitution() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.WisdomRequirement)
            { return character.Stats.Wisdom() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.CharismaRequirement)
            { return character.Stats.Charisma() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == FantasyRequirementTypes.MaxMagicRequirement)
            { return character.Stats.MaxMagic() >= requirement.Association.AssociatedValue; }

            return base.IsRequirementMet(character, requirement);
        }
    }
}