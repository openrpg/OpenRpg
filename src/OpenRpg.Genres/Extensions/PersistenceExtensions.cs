using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Types;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Persistence.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Genres.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;

namespace OpenRpg.Genres.Extensions
{
    public static class PersistenceExtensions
    {
        public static CharacterData ToDataModel(this ICharacter character)
        {
            var variables = new Dictionary<int, object>();

            if (character.Variables.HasEquipment())
            {
                var equipmentData = character.Variables.Equipment().ToDataModel();
                variables.Add(GenreEntityVariableTypes.Equipment, equipmentData);
            }
            
            if (character.Variables.HasInventory())
            {
                var inventoryData = character.Variables.Inventory().ToDataModel();
                variables.Add(GenreEntityVariableTypes.Inventory, inventoryData);
            }
            
            if (character.Variables.HasRace())
            {
                var raceTemplateId = character.Variables.Race().Id;
                variables.Add(GenreEntityVariableTypes.Race, raceTemplateId);
            }
            
            if (character.Variables.HasGender())
            {
                var genderId = character.Variables.Gender();
                variables.Add(GenreEntityVariableTypes.Gender, genderId);
            }
            
            if (character.Variables.HasClass())
            {
                var classData = character.Variables.Class().ToDataModel();
                variables.Add(GenreEntityVariableTypes.Class, classData);
            }
            
            if (character.Variables.HasMultiClass())
            {
                var multiClassData = character.Variables.MultiClass().ToDataModel();
                variables.Add(GenreEntityVariableTypes.MultiClasses, multiClassData);
            }
            
            return new CharacterData(character.UniqueId,
                character.NameLocaleId, character.DescriptionLocaleId, character.State, variables);
        }

        public static ItemData ToDataModel(this IUniqueItem item)
        {
            if (item == null) { return null; }
            return new ItemData(item.UniqueId,
                item.Template.Id,
                item.Modifications.Select(x => x.Id).ToArray(),
                item.Variables);
        }

        public static InventoryData ToDataModel(this IInventory inventory)
        {
            var items = inventory.Items.Select(x => (x as IUniqueItem).ToDataModel()).ToArray();
            return new InventoryData(items, inventory.Variables);
        }

        public static EquipmentData ToDataModel(this IEquipment equipment)
        {
            var slots = equipment.Slots
                .ToDictionary(x => x.Key,
                    x => (x.Value.SlottedItem as IUniqueItem).ToDataModel());
            
            return new EquipmentData(slots, equipment.Variables);
        }
        
        public static ClassData ToDataModel(this IClass @class)
        { return new ClassData(@class.Template.Id, @class.Variables); }
        
        public static MultiClassData ToDataModel(this IMultiClass multiClass)
        { return new MultiClassData(multiClass.Classes.Select(x => x.ToDataModel()).ToArray()); }
    }
}