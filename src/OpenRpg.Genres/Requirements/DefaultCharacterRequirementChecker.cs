using System.Linq;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.TradeSkills.Extensions;
using OpenRpg.Quests;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Genres.Requirements
{
    public class DefaultCharacterRequirementChecker : ICharacterRequirementChecker
    {
        public virtual bool IsRequirementMet(ICharacter character, Requirement requirement)
        {
            if (requirement.RequirementType == GenreRequirementTypes.RaceRequirement)
            {
                if (!character.Variables.HasRace())
                { return false; }
                
                return character.Variables.Race().Id == requirement.AssociatedId;
            }

            if (requirement.RequirementType == GenreRequirementTypes.ClassRequirement)
            {
                if (character.Variables.HasRace())
                {
                    var classDetails = character.Variables.Class();
                    if (classDetails.Template.Id == requirement.AssociatedId)
                    { return classDetails.Variables.Level() >= requirement.AssociatedValue; }
                }

                if (character.Variables.HasMultiClass())
                {
                    var multiClass = character.Variables.MultiClass();
                    var possibleClass = multiClass.GetClass(requirement.AssociatedId);
                    if (possibleClass != null)
                    { return possibleClass.Variables.Level() >= requirement.AssociatedValue; }
                }

                return false;
            }

            if (requirement.RequirementType == GenreRequirementTypes.GenderRequirement)
            {
                if (!character.Variables.HasGender())
                { return false; }
                
                return character.Variables.Gender() == requirement.AssociatedId;
            }
            
            if (requirement.RequirementType == GenreRequirementTypes.EquipmentItemRequirement)
            {
                if (!character.Variables.HasEquipment())
                { return false; }

                return character.Variables.Equipment().Slots.Values
                    .Any(x => x.SlottedItem?.Template.Id == requirement.AssociatedId);
            }
            
            if (requirement.RequirementType == GenreRequirementTypes.TradeSkillRequirement)
            {
                if (!character.Variables.HasTradeSkillState())
                { return false; }

                var tradeSkills = character.Variables.TradeSkillState();
                if (!tradeSkills.ContainsKey(requirement.AssociatedId))
                { return false; }

                return tradeSkills[requirement.AssociatedId] >= requirement.AssociatedValue;
            }
            
            if (requirement.RequirementType == GenreRequirementTypes.InventoryItemRequirement)
            {
                if (!character.Variables.HasInventory())
                { return false; }

                return character.Variables.Inventory()
                    .HasItem(requirement.AssociatedId, requirement.AssociatedValue);
            }

            if(requirement.RequirementType == GenreRequirementTypes.MaxHealthRequirement)
            { return character.Stats.MaxHealth() >= requirement.AssociatedValue; }
            
            return true;
        }

        public virtual bool IsRequirementMet(IQuestState state, Requirement requirement)
        {
            if (requirement.RequirementType == GenreRequirementTypes.QuestStateRequirement)
            {
                var questState = state.GetQuestState(requirement.AssociatedId);
                return requirement.AssociatedValue == questState;
            }
            
            return true;
        }
    
        public virtual bool IsRequirementMet(ITriggerStateVariables state, Requirement requirement)
        {
            if (requirement.RequirementType == GenreRequirementTypes.TriggerRequirement)
            {
                var hasTrigger = state.ContainsKey(requirement.AssociatedId);
                var triggerState = (requirement.AssociatedValue == 1);
                if(requirement.AssociatedValue == 0 && !hasTrigger) { return true; }
                
                return state[requirement.AssociatedId] == triggerState;
            }
            
            return true;
        }
    }
}