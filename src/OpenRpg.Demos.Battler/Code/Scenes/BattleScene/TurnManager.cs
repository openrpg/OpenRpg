using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class TurnManager
{
    private static readonly Random _rng = new();
    private List<BattleEntity> _party;
    private List<BattleEntity> _enemies;
    private double _phaseTimer;

    public enum Phase { Idle, TurnDwell, TargetFlash, GameOver }
    public Phase CurrentPhase { get; private set; } = Phase.Idle;
    public BattleEntity CurrentAttacker { get; private set; }
    public BattleEntity CurrentTarget { get; private set; }
    public Team WinningTeam { get; private set; }
    public int CurrentTurnIndex { get; private set; } = -1;
    public string LastActionMessage { get; private set; } = "";
    public List<BattleEntity> TurnOrder { get; private set; } = [];

    public void Start(List<BattleEntity> party, List<BattleEntity> enemies)
    {
        _party = party;
        _enemies = enemies;
        TurnOrder = party.Concat(enemies).OrderByDescending(e => e.Initiative).ToList();
        CurrentTurnIndex = -1;
        CurrentPhase = Phase.Idle;
        _phaseTimer = 0;
        LastActionMessage = "";
    }

    public void Update(double dt)
    {
        switch (CurrentPhase)
        {
            case Phase.Idle:
                if (AdvanceTurn())
                {
                    CurrentPhase = Phase.TurnDwell;
                    _phaseTimer = 0;
                }
                break;

            case Phase.TurnDwell:
                _phaseTimer += dt;
                if (_phaseTimer >= 0.8)
                {
                    ApplyDamage();
                    CurrentPhase = Phase.TargetFlash;
                    _phaseTimer = 0;
                }
                break;

            case Phase.TargetFlash:
                _phaseTimer += dt;
                if (_phaseTimer >= 0.3)
                {
                    if (CheckGameOver())
                        CurrentPhase = Phase.GameOver;
                    else
                    {
                        CurrentPhase = Phase.Idle;
                        _phaseTimer = 0;
                    }
                }
                break;
        }
    }

    private bool AdvanceTurn()
    {
        var count = TurnOrder.Count;
        for (var i = 0; i < count; i++)
        {
            CurrentTurnIndex = (CurrentTurnIndex + 1) % count;
            CurrentAttacker = TurnOrder[CurrentTurnIndex];
            if (!CurrentAttacker.IsAlive) continue;

            var targets = CurrentAttacker.Team == Team.Player ? _enemies : _party;
            var aliveTargets = targets.Where(e => e.IsAlive).ToList();
            if (aliveTargets.Count == 0) continue;
            CurrentTarget = aliveTargets[_rng.Next(aliveTargets.Count)];
            return true;
        }
        return false;
    }

    private void ApplyDamage()
    {
        var damage = CurrentAttacker.AttackDamage > 0
            ? CurrentAttacker.AttackDamage
            : _rng.Next(5, 15);
        CurrentTarget.Entity.State.DeductHealth(damage);
        LastActionMessage = $"{NormalizeName(CurrentAttacker.Name)} attacks {NormalizeName(CurrentTarget.Name)} for {damage} damage";
    }

    private bool CheckGameOver()
    {
        if (_party.All(e => !e.IsAlive))
        {
            WinningTeam = Team.Enemy;
            return true;
        }
        if (_enemies.All(e => !e.IsAlive))
        {
            WinningTeam = Team.Player;
            return true;
        }
        return false;
    }

    private static string NormalizeName(string name)
    {
        return Regex.Replace(name, "(?<=[a-z])(?=[A-Z])", " ");
    }
}
