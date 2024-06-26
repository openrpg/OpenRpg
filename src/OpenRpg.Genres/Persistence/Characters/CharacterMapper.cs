using System;
using System.Linq;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Races;
using OpenRpg.Core.State.Entity;
using OpenRpg.Core.Variables.Entity;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Genres.Persistence.Characters
{
    public abstract class CharacterMapper : ICharacterMapper
    {
        public IItemMapper ItemMapper { get; }
        public IClassMapper ClassMapper { get; }
        public IMultiClassMapper MultiClassMapper { get; }
        public IEquipmentMapper EquipmentMapper { get; }
        public IInventoryMapper InventoryMapper { get; }

        protected CharacterMapper(IItemMapper itemMapper, IClassMapper classMapper, IMultiClassMapper multiClassMapper, IEquipmentMapper equipmentMapper, IInventoryMapper inventoryMapper)
        {
            ItemMapper = itemMapper;
            ClassMapper = classMapper;
            MultiClassMapper = multiClassMapper;
            EquipmentMapper = equipmentMapper;
            InventoryMapper = inventoryMapper;
        }

        public ICharacter Map(CharacterData data)
        {
            var entityVariables = new DefaultEntityVariables();

            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Gender))
            {
                var genderId = Convert.ToByte(data.Variables[GenreEntityVariableTypes.Gender]);
                entityVariables.Gender(genderId);
            }

            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Race))
            {
                var raceTemplate = GetRaceTemplateFor(Convert.ToInt32(data.Variables[GenreEntityVariableTypes.Race]));
                entityVariables.Race(raceTemplate);
            }

            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Class))
            {
                var classTemplate = ClassMapper.Map(data.Variables[GenreEntityVariableTypes.Class] as ClassData);
                entityVariables.Class(classTemplate);
            }

            if (data.Variables.ContainsKey(GenreEntityVariableTypes.MultiClasses))
            {
                var multiClass = MultiClassMapper.Map(data.Variables[GenreEntityVariableTypes.MultiClasses] as MultiClassData);
                entityVariables.MultiClass(multiClass);
            }

            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Equipment))
            {
                var equipment = EquipmentMapper.Map(data.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData);
                entityVariables.Equipment(equipment);
            }
            
            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Inventory))
            {
                var inventory = InventoryMapper.Map(data.Variables[GenreEntityVariableTypes.Inventory] as InventoryData);
                entityVariables.Inventory(inventory);
            }
            
            var characterState = new DefaultEntityStateVariables(data.StateVariables
                .ToDictionary(x => x.Key, x => x.Value));

            
            return InitializeCharacter(data, characterState, entityVariables);
        }

        public virtual ICharacter InitializeCharacter(CharacterData data, IEntityStateVariables state, IEntityVariables variables)
        {
            return new DefaultCharacter
            {
                NameLocaleId = data.NameLocaleId,
                DescriptionLocaleId = data.DescriptionLocaleId,
                UniqueId = data.Id,
                Variables = variables,
                State = state
            };
        }
        
        public abstract IRaceTemplate GetRaceTemplateFor(int raceTemplateId);
    }
}