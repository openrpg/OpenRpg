using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Types;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Persistence.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Genres.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Builders
{
    public class CharacterBuilder
    {
        public ICharacterMapper CharacterMapper { get; }
        public IRandomizer Randomizer { get; }

        protected int _raceId, _classId, _classLevels;
        protected byte _genderId;
        protected string _name, _description;
        protected Dictionary<int, float> _state;

        protected Dictionary<int, IUniqueItem> _equipment;
        protected List<IUniqueItem> _inventory;
        
        protected Dictionary<int,object> _variables;

        public CharacterBuilder(ICharacterMapper characterMapper, IRandomizer randomizer)
        {
            CharacterMapper = characterMapper;
            Randomizer = randomizer;
        }

        public virtual CharacterBuilder CreateNew()
        {
            _raceId = _classId = _classLevels = _genderId = 0;
            _name = _description = string.Empty;
            _state = new Dictionary<int, float>();
            _equipment = new Dictionary<int, IUniqueItem>();
            _inventory = new List<IUniqueItem>();
            _variables = new Dictionary<int, object>();
            return this;
        }

        public CharacterBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        
        public CharacterBuilder WithNameLocale(string nameLocaleId)
        { return WithName(nameLocaleId); }
        
        public CharacterBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        
        public CharacterBuilder WithDescriptionLocale(string nameLocaleId)
        { return WithDescription(nameLocaleId); }

        public CharacterBuilder WithRaceId(int raceId)
        {
            _raceId = raceId;
            return this;
        }

        public CharacterBuilder WithClassId(int classId, int level)
        {
            _classId = classId;
            _classLevels = level;
            return this;
        }

        public CharacterBuilder AsMale()
        {
            _genderId = 1;
            return this;
        }

        public CharacterBuilder AsFemale()
        {
            _genderId = 2;
            return this;
        }

        public CharacterBuilder WithGender(byte genderId)
        {
            _genderId = genderId;
            return this;
        }

        public CharacterBuilder WithEquipment(int slotType, IUniqueItem item)
        {
            _equipment[slotType] = item;
            return this;
        }
        
        public CharacterBuilder WithInventoryItem(IUniqueItem item)
        {
            _inventory.Add(item);
            return this;
        }
        
        public CharacterBuilder WithState(int stateTypeId, float value)
        {
            _state[stateTypeId] = value;
            return this;
        }

        public T As<T>() where T : CharacterBuilder
        { return this as T; }
        
        protected virtual void RandomizeDefaults() {}
        protected virtual void PostProcessCharacter(ICharacter character){}
        protected virtual void PreCreateCharacterData(){}

        protected void ProcessEquipmentToVariables()
        {
            if (_equipment.Count == 0) { return; }
            
            var equipmentStore = _equipment
                .ToDictionary(x => x.Key, x => x.Value?.ToDataModel() ?? null);
                
            _variables.Add(GenreEntityVariableTypes.Equipment, new EquipmentData(equipmentStore));
        }
        
        protected void ProcessInventoryToVariables()
        {
            if (_inventory.Count == 0) { return; }
            
            var persistedInventory = _inventory.Select(x => x.ToDataModel()).ToArray();
            _variables.Add(GenreEntityVariableTypes.Inventory, new InventoryData(persistedInventory));
        }
        
        protected void ProcessRaceToVariables()
        {
            if (_raceId == 0) { return; }
            _variables.Add(GenreEntityVariableTypes.Race, _raceId);
        }        
        
        protected void ProcessGenderToVariables()
        {
            if (_genderId == 0) { return; }
            _variables.Add(GenreEntityVariableTypes.Gender, _genderId);
        }
        
        protected void ProcessClassToVariables()
        {
            if (_classId == 0) { return; }
            var classVars = new Dictionary<int, object>();
            classVars.Add(ClassVariableTypes.Level, _classLevels);
            _variables.Add(GenreEntityVariableTypes.Class, new ClassData(_classId, classVars));
        }
        
        public virtual CharacterData CreateCharacterData()
        {
            ProcessEquipmentToVariables();
            ProcessInventoryToVariables();
            ProcessRaceToVariables();
            ProcessClassToVariables();
            ProcessGenderToVariables();

            return new CharacterData(Guid.NewGuid(), _name, _description, _state, _variables);
        }
        
        public ICharacter Build()
        {
            RandomizeDefaults();
            
            if (string.IsNullOrEmpty(_name))
            { _name = "Unknown Name"; }

            PreCreateCharacterData();
            var characterData = CreateCharacterData();
            var character = CharacterMapper.Map(characterData);
            PostProcessCharacter(character);

            return character;
        }
    }
}