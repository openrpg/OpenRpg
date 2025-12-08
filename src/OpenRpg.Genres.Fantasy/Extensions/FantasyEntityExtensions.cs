using OpenRpg.Entities.Entity;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class FantasyEntityExtensions
    {
        extension(Entity entity)
        {
            public float ManaPercentage => entity.State.Mana / entity.Stats.MaxMana;
        }
    }
}