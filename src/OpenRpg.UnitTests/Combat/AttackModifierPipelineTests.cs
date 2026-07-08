using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Modifiers;
using OpenRpg.Genres.Fantasy.Combat.Modifiers;
using OpenRpg.Genres.Fantasy.Types;
using Xunit;

namespace OpenRpg.UnitTests.Combat;

public class AttackModifierPipelineTests
{
    [Fact]
    public void apply_with_empty_chain_returns_attack_unchanged()
    {
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 20f) });
        var pipeline = new AttackModifierPipeline(new List<IAttackModifier>());

        var result = pipeline.Apply(attack);

        Assert.Equal(attack, result);
    }

    [Fact]
    public void apply_with_unconditional_modifier_applies_it()
    {
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 20f) });
        var modifier = new RemoveDamageTypeModifier(FantasyDamageTypes.FireDamage);
        var pipeline = new AttackModifierPipeline(new[] { modifier });

        var result = pipeline.Apply(attack);

        Assert.Empty(result.Damages);
    }

    [Fact]
    public void apply_with_skipped_modifier_does_not_apply_it()
    {
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 20f) });
        var modifier = new AlwaysSkipModifier();
        var pipeline = new AttackModifierPipeline(new[] { modifier });

        var result = pipeline.Apply(attack);

        Assert.Equal(attack.Damages.Count, result.Damages.Count);
        Assert.Equal(20f, result.Damages.First().Value);
    }

    [Fact]
    public void apply_runs_modifiers_in_chain_order()
    {
        var first = new AddConstantDamageModifier(10f);
        var second = new AddConstantDamageModifier(100f);
        var pipeline = new AttackModifierPipeline(new[] { first, second });
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 0f) });

        var result = pipeline.Apply(attack);

        Assert.Equal(110f, result.Damages.First().Value);
    }

    [Fact]
    public void apply_runs_modifiers_in_chain_order_reversed_produces_different_result()
    {
        var first = new AddConstantDamageModifier(10f);
        var second = new AddConstantDamageModifier(100f);
        var pipeline = new AttackModifierPipeline(new[] { second, first });
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 0f) });

        var result = pipeline.Apply(attack);

        Assert.Equal(110f, result.Damages.First().Value);
    }

    [Fact]
    public void apply_modifiers_returns_n_plus_one_steps_for_n_modifiers()
    {
        var modifiers = new IAttackModifier[]
        {
            new RemoveDamageTypeModifier(FantasyDamageTypes.FireDamage),
            new AddConstantDamageModifier(5f)
        };
        var pipeline = new AttackModifierPipeline(modifiers);
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 20f) });

        var steps = pipeline.ApplyModifiers(attack);

        Assert.Equal(3, steps.Count);
        Assert.Null(steps[0].Modifier);
        Assert.True(steps[0].WasApplied);
        Assert.Equal(modifiers[0], steps[1].Modifier);
        Assert.Equal(modifiers[1], steps[2].Modifier);
    }

    [Fact]
    public void apply_modifiers_marks_skipped_modifiers_with_was_applied_false()
    {
        IAttackModifier first = new AlwaysSkipModifier();
        IAttackModifier second = new AddConstantDamageModifier(50f);
        var pipeline = new AttackModifierPipeline(new[] { first, second });
        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 20f) });

        var steps = pipeline.ApplyModifiers(attack);

        Assert.True(steps[0].WasApplied);
        Assert.False(steps[1].WasApplied);
        Assert.True(steps[2].WasApplied);
        Assert.Equal(70f, steps[2].Snapshot.Damages.First().Value);
    }

    private class AlwaysSkipModifier : IAttackModifier
    {
        public bool ShouldApply(Attack attack) => false;
        public Attack ModifyValue(Attack attack) => attack;
    }

    private class AddConstantDamageModifier : IAttackModifier
    {
        private readonly float _amount;
        public AddConstantDamageModifier(float amount) { _amount = amount; }
        public bool ShouldApply(Attack attack) => true;
        public Attack ModifyValue(Attack attack) =>
            attack with
            {
                Damages = attack.Damages
                    .Select(d => d with { Value = d.Value + _amount })
                    .ToList()
            };
    }
}
