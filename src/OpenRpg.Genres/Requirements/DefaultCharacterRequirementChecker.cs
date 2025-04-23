using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.TradeSkills.Extensions;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Requirements
{
    public class DefaultCharacterRequirementChecker : ICharacterRequirementChecker
    {
        public virtual bool IsRequirementMet(Character character, Requirement requirement)
        {
            if (requirement.RequirementType == GenreRequirementTypes.RaceRequirement)
            {
                if (!character.Variables.HasRace())
                { return false; }

                var race = character.Variables.Race();
                return race.TemplateId == requirement.Association.AssociatedId;
            }

            if (requirement.RequirementType == GenreRequirementTypes.ClassRequirement)
            {
                if (character.Variables.HasRace())
                {
                    var classDetails = character.Variables.Class();
                    if (classDetails.TemplateId == requirement.Association.AssociatedId)
                    { return classDetails.Variables.Level() >= requirement.Association.AssociatedValue; }
                }

                if (character.Variables.HasMultiClass())
                {
                    var multiClass = character.Variables.MultiClass();
                    var possibleClass = multiClass.GetClass(requirement.Association.AssociatedId);
                    if (possibleClass != null)
                    { return possibleClass.Variables.Level() >= requirement.Association.AssociatedValue; }
                }

                return false;
            }

            if (requirement.RequirementType == GenreRequirementTypes.GenderRequirement)
            {
                if (!character.Variables.HasGender())
                { return false; }
                
                return character.Variables.Gender() == requirement.Association.AssociatedId;
            }
            
            if (requirement.RequirementType == GenreRequirementTypes.EquipmentItemRequirement)
            {
                if (!character.Variables.HasEquipment())
                { return false; }

                return character.Variables.Equipment().Slots.Values
                    .Any(x => x?.TemplateId == requirement.Association.AssociatedId);
            }
            
            if (requirement.RequirementType == GenreRequirementTypes.TradeSkillRequirement)
            {
                if (!character.Variables.HasTradeSkillState())
                { return false; }

                var tradeSkills = character.Variables.TradeSkillState();
                if (!tradeSkills.ContainsKey(requirement.Association.AssociatedId))
                { return false; }

                return tradeSkills[requirement.Association.AssociatedId] >= requirement.Association.AssociatedValue;
            }
            
            if (requirement.RequirementType == GenreRequirementTypes.InventoryItemRequirement)
            {
                if (!character.Variables.HasInventory())
                { return false; }

                return character.Variables.Inventory()
                    .HasItem(requirement.Association.AssociatedId, requirement.Association.AssociatedValue);
            }

            if(requirement.RequirementType == GenreRequirementTypes.MaxHealthRequirement)
            { return character.Stats.MaxHealth() >= requirement.Association.AssociatedValue; }
            
            if(requirement.RequirementType == GenreRequirementTypes.MaxStaminaRequirement)
            { return character.Stats.MaxStamina() >= requirement.Association.AssociatedValue; }
            
            return true;
        }

        public virtual bool IsRequirementMet(IQuestState state, Requirement requirement)
        {
            if (requirement.RequirementType == GenreRequirementTypes.QuestStateRequirement)
            {
                var questState = state.GetQuestState(requirement.Association.AssociatedId);
                return requirement.Association.AssociatedValue == questState;
            }
            
            return true;
        }
    
        public virtual bool IsRequirementMet(ITriggerStateVariables state, Requirement requirement)
        {
            if (requirement.RequirementType == GenreRequirementTypes.TriggerRequirement)
            {
                var hasTrigger = state.ContainsKey(requirement.Association.AssociatedId);
                var triggerState = (requirement.Association.AssociatedValue == 1);
                if(requirement.Association.AssociatedValue == 0 && !hasTrigger) { return true; }
                
                return state[requirement.Association.AssociatedId] == triggerState;
            }
            
            return true;
        }
    }
}