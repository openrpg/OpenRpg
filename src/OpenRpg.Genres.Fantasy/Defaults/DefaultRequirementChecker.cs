using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Quests.States;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultRequirementChecker : IRequirementChecker
    {
        public bool IsRequirementMet(ICharacter character, Requirement requirement)
        {
            if (requirement.RequirementType == RequirementTypes.RaceRequirement)
            { return character.Race.Id == requirement.AssociatedId; }
            
            if (requirement.RequirementType == RequirementTypes.ClassRequirement)
            { return character.Class.ClassTemplate.Id == requirement.AssociatedId; }
            
            if (requirement.RequirementType == RequirementTypes.LevelRequirement)
            { return character.Class.Level >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.GenderRequirement)
            { return character.GenderType == requirement.AssociatedId; }
            
            if(requirement.RequirementType == RequirementTypes.StrengthRequirement)
            { return character.Stats.Strength() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.DexterityRequirement)
            { return character.Stats.Dexterity() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.IntelligenceRequirement)
            { return character.Stats.Intelligence() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.ConstitutionRequirement)
            { return character.Stats.Constitution() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.WisdomRequirement)
            { return character.Stats.Wisdom() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.CharismaRequirement)
            { return character.Stats.Charisma() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.MaxMagicRequirement)
            { return character.Stats.MaxMagic() >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.MaxHealthRequirement)
            { return character.Stats.MaxHealth() >= requirement.AssociatedValue; }

            if (requirement.RequirementType == RequirementTypes.EquipmentItemRequirement)
            {
                return character.Equipment.GetEquipmentSlots().Any(x =>
                    x.SlotType == requirement.AssociatedId &&
                    x.SlottedItem.ItemTemplate.Id == requirement.AssociatedValue);
            }
            
            return true;
        }

        public bool IsRequirementMet(IGameState state, Requirement requirement)
        {
            if (requirement.RequirementType == RequirementTypes.TriggerRequirement)
            {
                var hasTrigger = state.Triggers.ContainsKey(requirement.AssociatedId);
                var triggerState = (requirement.AssociatedValue == 1);
                if(requirement.AssociatedValue == 0 && !hasTrigger) { return true; }
                
                return state.Triggers[requirement.AssociatedId] == triggerState;
            }

            if (requirement.RequirementType == RequirementTypes.QuestStateRequirement)
            {
                var hasQuestState = state.QuestStates.ContainsKey(requirement.AssociatedId);
                if(requirement.AssociatedValue == QuestStateTypes.QuestNotStarted && !hasQuestState) { return true; }

                return state.QuestStates[requirement.AssociatedId] == requirement.AssociatedValue;
            }
            
            return true;
        }
    }
}