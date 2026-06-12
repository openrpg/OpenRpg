using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

/// <summary>
/// Handles basic attack execution. Parallel to AbilityExecutor for consistency.
/// </summary>
public class BasicAttackExecutor
{
    private static readonly Random _rng = new();
    private readonly IEntityAttackGenerator _attackGenerator;
    private readonly IEntityAttackProcessor _attackProcessor;

    public record AttackResult(
        BattleEntity Target,
        int Damage,
        bool IsCrit,
        string Message);

    public BasicAttackExecutor(IEntityAttackGenerator attackGenerator, IEntityAttackProcessor attackProcessor)
    {
        _attackGenerator = attackGenerator;
        _attackProcessor = attackProcessor;
    }

    public AttackResult ExecuteBasicAttack(
        BattleEntity attacker,
        BattleEntity specificTarget,
        List<BattleEntity> enemyPool)
    {
        var aliveTargets = enemyPool.Where(e => e.IsAlive).ToList();

        BattleEntity target;
        if (specificTarget != null && specificTarget.IsAlive)
            target = specificTarget;
        else
            target = aliveTargets[_rng.Next(aliveTargets.Count)];

        var attack = _attackGenerator.GenerateAttack(attacker.Entity.Stats);
        var processed = _attackProcessor.ProcessAttack(attack, target.Entity.Stats);
        var totalDamage = (int)Math.Max(1, processed.DamageDone.Sum(d => d.Value));

        target.Entity.State.DeductHealth(totalDamage);

        var attackerName = UI.NameHelper.NormalizeName(attacker.Name);
        var targetName = UI.NameHelper.NormalizeName(target.Name);
        var message = CombatLogBuilder.BuildAttackMessage(attackerName, targetName, totalDamage, attack.IsCritical);

        return new AttackResult(target, totalDamage, attack.IsCritical, message);
    }
}
