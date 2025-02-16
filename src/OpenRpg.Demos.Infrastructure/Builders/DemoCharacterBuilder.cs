using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Utils;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Genres.Builders;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Populators.Entity.Stats;
using RandomNameGenerator;

namespace OpenRpg.Demos.Infrastructure.Builders
{
    public class DemoCharacterBuilder : CharacterBuilder
    {
        public IReadOnlyCollection<int> RaceTypes { get; }
        public IReadOnlyCollection<int> ClassTypes { get; }
        
        public IEntityStatPopulator StatPopulator { get; }
        public ITemplateAccessor TemplateAccessor { get; }
        
        public DemoCharacterBuilder(IRandomizer randomizer, IEntityStatPopulator statPopulator, ITemplateAccessor templateAccessor) : base(randomizer)
        {
            RaceTypes = typeof(RaceTypeLookups).GetTypeFieldsDictionary().Keys.Where(x => x > 0).ToArray();
            ClassTypes = typeof(ClassTypeLookups).GetTypeFieldsDictionary().Keys.Where(x => x > 0).ToArray();

            StatPopulator = statPopulator;
            TemplateAccessor = templateAccessor;
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
            StatPopulator.Populate(character.Stats, character.ComputeEffects(TemplateAccessor), null);
            
            var health = character.Stats.MaxHealth();
            var magic = character.Stats.MaxMagic();
            character.State.Health(health);
            character.State.Magic(magic);
        }
    }
}