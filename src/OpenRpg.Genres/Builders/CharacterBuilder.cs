using System;
using System.Collections.Generic;
using OpenRpg.Core.Utils;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Races;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Genres.Characters;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Builders
{
    public class CharacterBuilder
    {
        public IRandomizer Randomizer { get; }

        protected int _raceId, _classId, _classLevels;
        protected byte _genderId;
        protected string _name, _description;
        protected Dictionary<int, float> _state;

        protected Dictionary<int, ItemData> _equipment;
        protected List<ItemData> _inventory;
        
        protected EntityVariables _variables;

        public CharacterBuilder(IRandomizer randomizer)
        {
            Randomizer = randomizer;
        }

        public virtual CharacterBuilder CreateNew()
        {
            _raceId = _classId = _classLevels = _genderId = 0;
            _name = _description = string.Empty;
            _state = new Dictionary<int, float>();
            _equipment = new Dictionary<int, ItemData>();
            _inventory = new List<ItemData>();
            _variables = new EntityVariables();
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

        public CharacterBuilder WithEquipment(int slotType, ItemData itemDataHas)
        {
            _equipment[slotType] = itemDataHas;
            return this;
        }
        
        public CharacterBuilder WithInventoryItem(ItemData itemDataHas)
        {
            _inventory.Add(itemDataHas);
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

        protected virtual Equipment ProcessEquipment()
        {
            return new Equipment() { Slots = new EquipmentSlots(_equipment) };
        }
        
        protected virtual Inventory ProcessInventory()
        {
            return new Inventory() { Items = _inventory };
        }
        
        protected virtual ClassData ProcessClass()
        {
            var classData = new ClassData();
            if (_classId == 0) { return classData; }

            classData.TemplateId = _classId;
            classData.Variables.Level(_classLevels);
            return classData;
        }
        
        protected virtual RaceData ProcessRace()
        {
            var raceData = new RaceData();
            if (_raceId == 0) { return raceData; }

            raceData.TemplateId = _raceId;
            return raceData;
        }
        
        protected virtual void PreProcessCharacter() {}
        protected virtual void PostProcessCharacter(Character character) {}
        
        public virtual Character CreateCharacter()
        {
            _variables.Class(ProcessClass());
            _variables.Race(ProcessRace());
            _variables.Equipment(ProcessEquipment());
            _variables.Inventory(ProcessInventory());
            _variables.Gender(_genderId);

            return new Character()
            {
                UniqueId = Guid.NewGuid(),
                NameLocaleId = _name,
                DescriptionLocaleId = _description,
                Variables = _variables,
                State = new EntityStateVariables(_state)
            };
        }
        
        public Character Build()
        {
            RandomizeDefaults();
            
            if (string.IsNullOrEmpty(_name))
            { _name = "Unknown Name"; }

            PreProcessCharacter();
            var character = CreateCharacter();
            PostProcessCharacter(character);
            return character;
        }
    }
}