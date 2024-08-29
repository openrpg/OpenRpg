using System.Collections.Generic;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Genres.Builders;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using RandomNameGenerator;

namespace OpenRpg.Demos.Infrastructure.Builders
{
    public class DemoCharacterBuilder : CharacterBuilder
    {
        public ICollection<int> RaceTypes { get; }
        public ICollection<int> ClassTypes { get; }
        
        public DemoCharacterBuilder(IRandomizer randomizer) : base(randomizer)
        {
            RaceTypes = typeof(RaceTypeLookups).GetTypeFieldsDictionary().Keys;
            ClassTypes = typeof(ClassTypeLookups).GetTypeFieldsDictionary().Keys;
        }

        protected override void RandomizeDefaults()
        {
            if (_raceId == 0) { _raceId = Randomizer.TakeRandomFrom(RaceTypes); }
            if (_classId == 0) { _classId = Randomizer.TakeRandomFrom(ClassTypes); }
            if (_genderId == 0) { _genderId = (byte)Randomizer.Random(1,2); }
            if (_classLevels == 0) { _classLevels = Randomizer.Random(1,5); }
            if (string.IsNullOrEmpty(_name)) { _name = NameGenerator.Generate(_genderId == 1 ? Gender.Male : Gender.Female); }
        }

        protected override void PostProcessCharacter(Character character)
        {
            var health = character.Stats.MaxHealth();
            var magic = character.Stats.MaxMagic();
            character.State.Health(health);
            character.State.Magic(magic);
        }
    }
}