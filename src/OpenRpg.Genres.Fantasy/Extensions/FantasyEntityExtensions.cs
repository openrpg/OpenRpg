using OpenRpg.Entities.Entity;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class FantasyEntityExtensions
    {
        extension(EntityData entityData)
        {
            public float ManaPercentage => entityData.State.Mana / entityData.Stats.MaxMana;
        }
    }
}