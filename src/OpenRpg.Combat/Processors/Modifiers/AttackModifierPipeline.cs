using System.Collections.Generic;
using OpenRpg.Combat.Attacks;

namespace OpenRpg.Combat.Processors.Modifiers
{
    /// <summary>
    /// Runs a list of <see cref="IAttackModifier"/> in order against an <see cref="Attack"/>,
    /// honoring each modifier's <see cref="IAttackModifier.ShouldApply"/> short-circuit.
    /// </summary>
    public sealed class AttackModifierPipeline
    {
        public IReadOnlyList<IAttackModifier> Modifiers { get; }

        public AttackModifierPipeline(IReadOnlyList<IAttackModifier> modifiers)
        {
            Modifiers = modifiers;
        }

        public Attack Apply(Attack attack)
        {
            var current = attack;
            foreach (var modifier in Modifiers)
            {
                if (!modifier.ShouldApply(current)) { continue; }
                current = modifier.ModifyValue(current);
            }
            return current;
        }

        public IReadOnlyList<AttackModifierStep> ApplyModifiers(Attack attack)
        {
            var steps = new List<AttackModifierStep> { new(attack, null, true) };
            var current = attack;
            foreach (var modifier in Modifiers)
            {
                if (!modifier.ShouldApply(current))
                {
                    steps.Add(new AttackModifierStep(current, modifier, false));
                    continue;
                }
                current = modifier.ModifyValue(current);
                steps.Add(new AttackModifierStep(current, modifier, true));
            }
            return steps;
        }
    }
}
