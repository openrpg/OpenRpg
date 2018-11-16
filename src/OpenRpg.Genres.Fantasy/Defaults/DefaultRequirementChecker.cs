using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Player;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Genres.Fantasy.Types;

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
            { return character.Stats.AttributeStats.Strength >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.DexterityRequirement)
            { return character.Stats.AttributeStats.Dexterity >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.IntelligenceRequirement)
            { return character.Stats.AttributeStats.Intelligence >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.ConstitutionRequirement)
            { return character.Stats.AttributeStats.Constitution >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.WisdomRequirement)
            { return character.Stats.AttributeStats.Wisdom >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.CharismaRequirement)
            { return character.Stats.AttributeStats.Charisma >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.MaxMagicRequirement)
            { return character.Stats.VitalStats.MaxMagic >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.MaxHealthRequirement)
            { return character.Stats.VitalStats.MaxHealth >= requirement.AssociatedValue; }

            if (requirement.RequirementType == RequirementTypes.EquipmentItemRequirement)
            {
                return character.Equipment.GetEquipmentSlots().Any(x =>
                    x.SlotType == requirement.AssociatedId &&
                    x.SlottedItem.ItemTemplate.Id == requirement.AssociatedValue);
            }
            
            return true;
        }

        public bool IsRequirementMet(IPlayerState player, Requirement requirement)
        {
            if (requirement.RequirementType == RequirementTypes.TriggerRequirement)
            {
                var hasTrigger = player.Triggers.ContainsKey(requirement.AssociatedId);
                var triggerState = (requirement.AssociatedValue == 1);
                if(requirement.AssociatedValue == 0 && !hasTrigger) { return true; }
                
                return player.Triggers[requirement.AssociatedId] == triggerState;
            }

            if (requirement.RequirementType == RequirementTypes.QuestStateRequirement)
            {
                var hasQuestState = player.QuestStates.ContainsKey(requirement.AssociatedId);
                if(requirement.AssociatedValue == QuestStateTypes.QuestNotStarted && !hasQuestState) { return true; }

                return player.QuestStates[requirement.AssociatedId] == requirement.AssociatedValue;
            }
            
            return true;
        }
    }
}