using OpenRpg.Combat.Attacks;

namespace OpenRpg.Combat.Processors.Modifiers
{
    public interface IAttackModifier
    {
        bool ShouldApply(Attack attack);
        Attack ModifyValue(Attack attack);
    }
}