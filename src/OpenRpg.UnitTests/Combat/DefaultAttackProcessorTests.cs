using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using Xunit;

namespace OpenRpg.UnitTests.Combat;

public class DefaultAttackProcessorTests
{
    [Fact]
    public void should_subtract_per_type_defense_from_incoming_damage()
    {
        var stats = new EntityStatsVariables();
        stats.FireDefense = 10f;

        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 30f) });
        var processed = new DefaultAttackProcessor().ProcessAttack(attack, stats);

        var done = Assert.Single(processed.DamageDone);
        var defended = Assert.Single(processed.DamageDefended);

        Assert.Equal(FantasyDamageTypes.FireDamage, done.Type);
        Assert.Equal(20f, done.Value);
        Assert.Equal(10f, defended.Value);
    }

    [Fact]
    public void should_clamp_damage_done_at_zero_when_defense_meets_or_exceeds_damage()
    {
        var stats = new EntityStatsVariables();
        stats.FireDefense = 50f;

        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 20f) });
        var processed = new DefaultAttackProcessor().ProcessAttack(attack, stats);

        var done = Assert.Single(processed.DamageDone);
        var defended = Assert.Single(processed.DamageDefended);

        Assert.Equal(0f, done.Value);
        Assert.Equal(20f, defended.Value);
    }

    [Fact]
    public void should_handle_multi_type_attack_independently_per_type()
    {
        var stats = new EntityStatsVariables();
        stats.FireDefense = 5f;
        stats.IceDefense = 15f;

        var attack = new Attack(false, new[]
        {
            new Damage(FantasyDamageTypes.SlashingDamage, 8f),
            new Damage(FantasyDamageTypes.FireDamage, 25f),
            new Damage(FantasyDamageTypes.IceDamage, 20f),
        });
        var processed = new DefaultAttackProcessor().ProcessAttack(attack, stats);

        var byType = processed.DamageDone.ToDictionary(d => d.Type, d => d.Value);
        Assert.Equal(8f, byType[FantasyDamageTypes.SlashingDamage]);
        Assert.Equal(20f, byType[FantasyDamageTypes.FireDamage]);
        Assert.Equal(5f, byType[FantasyDamageTypes.IceDamage]);
    }

    [Fact]
    public void should_read_defense_via_extension_property_not_damage_type_key()
    {
        var stats = new EntityStatsVariables();
        stats.FireDefense = 10f;

        var wrongKey = FantasyDamageTypes.FireDamage;
        stats.InternalVariables[wrongKey] = 10f;

        var attack = new Attack(false, new[] { new Damage(FantasyDamageTypes.FireDamage, 30f) });
        var processed = new DefaultAttackProcessor().ProcessAttack(attack, stats);

        var done = Assert.Single(processed.DamageDone);
        Assert.Equal(20f, done.Value);
    }
}
