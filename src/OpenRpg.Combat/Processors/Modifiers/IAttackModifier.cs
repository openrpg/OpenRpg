using OpenRpg.Combat.Attacks;

namespace OpenRpg.Genres.Fantasy.Combat.Modifiers
{
    public interface IAttackModifier
    {
        bool ShouldApply(Attack attack);
        Attack ModifyValue(Attack attack);
    }
}