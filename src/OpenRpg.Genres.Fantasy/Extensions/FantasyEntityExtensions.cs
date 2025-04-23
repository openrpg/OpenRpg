using OpenRpg.Entities.Entity;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class FantasyEntityExtensions
    {
        public static float GetManaPercentage(this Entity entity)
        { return (float)entity.State.Mana() / entity.Stats.MaxMana(); }
    }
}