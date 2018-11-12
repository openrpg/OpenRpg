using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Damage;
using OpenRpg.Genres.Fantasy.Stats.Defense;
using OpenRpg.Genres.Fantasy.Stats.Vitals;

namespace OpenRpg.Genres.Fantasy.Stats
{
    public interface ICharacterStats
    {
        IAttributeStats AttributeStats { get; }
        IVitalStats VitalStats { get; }
        IDamageStats DamageStats { get; }
        IDefenseStats DefenseStats { get; }
    }
}