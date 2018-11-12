using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Damage;
using OpenRpg.Genres.Fantasy.Stats.Defense;
using OpenRpg.Genres.Fantasy.Stats.Vitals;

namespace OpenRpg.Genres.Fantasy.Stats
{
    public class CharacterStats : ICharacterStats
    {
        public IAttributeStats AttributeStats { get; set; }
        public IVitalStats VitalStats { get; set; }
        public IDamageStats DamageStats { get; set; }
        public IDefenseStats DefenseStats { get; set; }
    }
}