using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Demos.Infrastructure.Data;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Requirements;

namespace OpenRpg.Demos.Infrastructure.Services
{
    /// <summary>
    /// 1v-many combat sandbox that mirrors the Battler demo's <c>AbilityExecutor</c> +
    /// <c>TargetResolver</c> end-to-end. Casters use the library's attack generators
    /// and processors; targets take defense-subtracted damage from the
    /// <see cref="DefaultAttackProcessor"/>; heals are a flat-value restore.
    /// </summary>
    public class AbilityExecutionDemoService : IAbilityExecutionDemoService
    {
        private const int MaxLogEntries = 50;

        private readonly IEntityAttackGenerator _attackGenerator;
        private readonly IEntityAttackProcessor _attackProcessor;
        private readonly ICharacterRequirementChecker _requirementChecker;
        private readonly List<AbilityTemplate> _abilityTemplates;
        private readonly Dictionary<int, List<Requirement>> _requirementsByTemplateId;
        private readonly List<Combatant> _party;
        private readonly List<Combatant> _enemies;
        private readonly List<string> _combatLog = new();

        public IReadOnlyList<CasterSnapshot> Party => _party.Select((c, i) => MakeCasterSnapshot(c, i)).ToList();
        public IReadOnlyList<TargetSnapshot> Enemies => _enemies.Select((c, i) => MakeTargetSnapshot(c, i)).ToList();
        public IReadOnlyList<AbilityOption> AvailableAbilities { get; }
        public IReadOnlyList<string> CombatLog => _combatLog;
        public IReadOnlyList<int> LastCastEnemyTargetIndexes { get; private set; } = Array.Empty<int>();
        public IReadOnlyList<int> LastCastAllyTargetIndexes { get; private set; } = Array.Empty<int>();
        public int LastCastAbilityId { get; private set; }
        public SandboxSnapshot InitialState { get; }

        public AbilityExecutionDemoService(
            IEntityAttackGenerator attackGenerator,
            IEntityAttackProcessor attackProcessor,
            ICharacterRequirementChecker requirementChecker)
        {
            _attackGenerator = attackGenerator;
            _attackProcessor = attackProcessor;
            _requirementChecker = requirementChecker;

            _abilityTemplates = new AbilityTemplateDataGenerator().GenerateData().ToList();
            _requirementsByTemplateId = _abilityTemplates.ToDictionary(
                t => t.Id,
                t => t.Variables.Requirements?.ToList() ?? new List<Requirement>());

            _party = SeedParty();
            _enemies = SeedEnemies();

            AvailableAbilities = _abilityTemplates.Select(MakeAbilityOption).ToList();
            InitialState = new SandboxSnapshot(Party, Enemies);
        }

        public IReadOnlyList<AbilityOption> GetAbilitiesForCaster(int casterIndex)
        {
            var caster = _party.ElementAtOrDefault(casterIndex);
            if (caster == null) { return Array.Empty<AbilityOption>(); }
            return AvailableAbilities
                .Where(a => CasterMeetsClassRequirement(caster, a.Id))
                .ToList();
        }

        public bool CanCasterUseAbility(int casterIndex, int abilityTemplateId)
        {
            var caster = _party.ElementAtOrDefault(casterIndex);
            if (caster == null || !caster.IsAlive) { return false; }
            var ability = AvailableAbilities.FirstOrDefault(a => a.Id == abilityTemplateId);
            if (ability == null) { return false; }
            if (ability.ManaCost > caster.CurrentMana) { return false; }
            return CasterMeetsClassRequirement(caster, abilityTemplateId);
        }

        private bool CasterMeetsClassRequirement(Combatant caster, int abilityTemplateId)
        {
            if (!_requirementsByTemplateId.TryGetValue(abilityTemplateId, out var requirements) || requirements.Count == 0)
            { return true; }
            var stubCharacter = new Character { Variables = new EntityVariables() };
            stubCharacter.Variables.Class = new ClassData { TemplateId = caster.ClassId };
            return _requirementChecker.AreRequirementsMet(stubCharacter, requirements);
        }

        public void Reset()
        {
            foreach (var combatant in _party) { combatant.RestoreFromSeed(); }
            foreach (var combatant in _enemies) { combatant.RestoreFromSeed(); }
            _combatLog.Clear();
            LastCastEnemyTargetIndexes = Array.Empty<int>();
            LastCastAllyTargetIndexes = Array.Empty<int>();
            LastCastAbilityId = 0;
            Log("RESET: sandbox restored from seed");
        }

        public bool CastAbility(int casterIndex, int abilityTemplateId)
        {
            var caster = _party.ElementAtOrDefault(casterIndex);
            if (caster == null) { Log($"  ! no caster at index {casterIndex}"); return false; }
            var template = _abilityTemplates.FirstOrDefault(t => t.Id == abilityTemplateId);
            if (template == null) { Log($"  ! no ability with id {abilityTemplateId}"); return false; }
            LastCastAbilityId = abilityTemplateId;

            Log($"CAST: {caster.Name} attempts {template.NameLocaleId} (id={template.Id})");
            if (!caster.IsAlive) { Log("  ! caster is dead"); return false; }

            var manaCost = template.Variables.GetIntOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0);
            if (caster.CurrentMana < manaCost)
            {
                Log($"  ! not enough mana (have {caster.CurrentMana}, need {manaCost})");
                return false;
            }

            if (_requirementsByTemplateId.TryGetValue(abilityTemplateId, out var requirements))
            {
                var stubCharacter = new Character { Variables = new EntityVariables() };
                stubCharacter.Variables.Class = new ClassData { TemplateId = caster.ClassId };
                if (requirements.Count > 0 && !_requirementChecker.AreRequirementsMet(stubCharacter, requirements))
                {
                    Log($"  ! {caster.Name} does not meet the class requirements (class id {caster.ClassId})");
                    return false;
                }
            }

            var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
            var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);
            var isHealing = template.Variables.Damage.Type == FantasyDamageTypes.LightDamage;

            var pool = isHealing ? (IReadOnlyList<Combatant>)_party : _enemies;
            Log($"  - target pool: {(isHealing ? "party" : "enemies")}, TargetType={targetType}, TargetCount={targetCount}");

            var aliveInPool = pool.Where(e => e.IsAlive).ToList();
            var resolved = targetType == CombatTargetTypes.MultipleTarget
                ? aliveInPool.Take(targetCount).ToList()
                : aliveInPool.Take(1).ToList();

            if (resolved.Count == 0)
            {
                Log("  ! no valid targets in pool");
                return false;
            }

            if (isHealing)
            {
                ApplyHealing(caster, template, resolved, manaCost);
                LastCastAllyTargetIndexes = resolved.Select(t => _party.IndexOf(t)).ToList();
                LastCastEnemyTargetIndexes = Array.Empty<int>();
            }
            else
            {
                ApplyDamage(caster, template, resolved, manaCost);
                LastCastEnemyTargetIndexes = resolved.Select(t => _enemies.IndexOf(t)).ToList();
                LastCastAllyTargetIndexes = Array.Empty<int>();
            }

            return true;
        }

        private void ApplyDamage(Combatant caster, AbilityTemplate template, List<Combatant> targets, int manaCost)
        {
            var baseDamage = template.Variables.Damage;
            var attack = _attackGenerator.GenerateAttack(baseDamage, caster.Stats);
            var targetCount = targets.Count;
            var splitFraction = targetCount > 0 ? 1.0f / targetCount : 0f;
            Log($"  - generated attack: crit={attack.IsCritical}, base={baseDamage.Value} type={baseDamage.Type}, split 1/{targetCount} across {targetCount} target(s)");

            foreach (var target in targets)
            {
                var processed = _attackProcessor.ProcessAttack(attack, target.Stats);
                var rawDone = processed.DamageDone.Sum(d => d.Value);
                var splitDone = rawDone * splitFraction;
                var finalDamage = Math.Max(1, (int)Math.Round(splitDone));
                var prevHp = target.CurrentHp;
                target.CurrentHp = Math.Max(0, target.CurrentHp - finalDamage);
                var critPrefix = attack.IsCritical ? "CRIT! " : "";
                Log($"    > {critPrefix}{target.Name} takes {finalDamage} damage (HP {prevHp} -> {target.CurrentHp}){(target.IsAlive ? "" : " (killed)")}");
            }

            caster.CurrentMana = Math.Max(0, caster.CurrentMana - manaCost);
            Log($"  - {caster.Name} mana: {caster.CurrentMana + manaCost} -> {caster.CurrentMana}");
        }

        private void ApplyHealing(Combatant caster, AbilityTemplate template, List<Combatant> targets, int manaCost)
        {
            var healAmount = Math.Max(1, (int)template.Variables.Damage.Value);
            Log($"  - heal value: {healAmount} HP, applied to {targets.Count} target(s)");

            foreach (var target in targets)
            {
                var prevHp = target.CurrentHp;
                target.CurrentHp = Math.Min(target.MaxHp, target.CurrentHp + healAmount);
                var actualHeal = target.CurrentHp - prevHp;
                Log($"    > {target.Name} heals {actualHeal} HP (HP {prevHp} -> {target.CurrentHp})");
            }

            caster.CurrentMana = Math.Max(0, caster.CurrentMana - manaCost);
            Log($"  - {caster.Name} mana: {caster.CurrentMana + manaCost} -> {caster.CurrentMana}");
        }

        private void Log(string message)
        {
            var stamp = DateTime.Now.ToString("HH:mm:ss");
            _combatLog.Insert(0, $"[{stamp}] {message}");
            if (_combatLog.Count > MaxLogEntries) { _combatLog.RemoveRange(MaxLogEntries, _combatLog.Count - MaxLogEntries); }
        }

        private static CasterSnapshot MakeCasterSnapshot(Combatant combatant, int index) =>
            combatant.IsCaster
                ? new CasterSnapshot(index, combatant.Name, combatant.ClassId, combatant.ClassName,
                    combatant.MaxHp, combatant.CurrentHp, combatant.MaxMana, combatant.CurrentMana, combatant.IsAlive)
                : new CasterSnapshot(index, combatant.Name, 0, "Enemy", combatant.MaxHp, combatant.CurrentHp, 0, 0, combatant.IsAlive);

        private static TargetSnapshot MakeTargetSnapshot(Combatant combatant, int index) =>
            new TargetSnapshot(index, combatant.Name, combatant.MaxHp, combatant.CurrentHp, combatant.IsAlive);

        private static AbilityOption MakeAbilityOption(AbilityTemplate template) =>
            new AbilityOption(
                template.Id,
                template.NameLocaleId,
                template.DescriptionLocaleId,
                template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1),
                template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1),
                template.Variables.GetIntOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0),
                template);

        private static List<Combatant> SeedParty()
        {
            return new List<Combatant>
            {
                new Combatant("Aldric", 0, ClassTypeLookups.Fighter, "Fighter", 120, 20, isEnemy: false)
                {
                    Stats = FighterStats(slashing: 5, blunt: 1, piercing: 1)
                },
                new Combatant("Mira", 1, ClassTypeLookups.Mage, "Mage", 80, 100, isEnemy: false)
                {
                    Stats = MageStats(fire: 8, ice: 8)
                },
                new Combatant("Tess", 2, ClassTypeLookups.Mage, "Mage", 80, 100, isEnemy: false)
                {
                    Stats = MageStats(fire: 4, ice: 4)
                },
                new Combatant("Borin", 3, ClassTypeLookups.Fighter, "Fighter", 140, 20, isEnemy: false)
                {
                    Stats = FighterStats(slashing: 4, blunt: 2, piercing: 2)
                },
            };
        }

        private static List<Combatant> SeedEnemies()
        {
            return new List<Combatant>
            {
                new Combatant("Goblin", 0, 0, "Enemy", 30, 0, isEnemy: true)
                {
                    Stats = EnemyStats(slashing: 2, blunt: 1, piercing: 1, fire: 1, ice: 1, wind: 1, earth: 1, light: 1, dark: 1)
                },
                new Combatant("Bat", 1, 0, "Enemy", 15, 0, isEnemy: true)
                {
                    Stats = EnemyStats(slashing: 1, blunt: 1, piercing: 1, fire: 1, ice: 1, wind: 2, earth: 1, light: 1, dark: 1)
                },
                new Combatant("Wolf", 2, 0, "Enemy", 40, 0, isEnemy: true)
                {
                    Stats = EnemyStats(slashing: 2, blunt: 1, piercing: 1, fire: 1, ice: 1, wind: 1, earth: 4, light: 1, dark: 1)
                },
                new Combatant("Skeleton", 3, 0, "Enemy", 50, 0, isEnemy: true)
                {
                    Stats = EnemyStats(slashing: 5, blunt: 3, piercing: 3, fire: 1, ice: 1, wind: 1, earth: 2, light: 1, dark: 2)
                },
                new Combatant("Slime", 4, 0, "Enemy", 20, 0, isEnemy: true)
                {
                    Stats = EnemyStats(slashing: 1, blunt: 5, piercing: 1, fire: 1, ice: 1, wind: 1, earth: 1, light: 1, dark: 1)
                },
            };
        }

        private static EntityStatsVariables FighterStats(float slashing, float blunt, float piercing)
        {
            var stats = new EntityStatsVariables();
            stats.SlashingDamage = slashing;
            stats.BluntDamage = blunt;
            stats.PiercingDamage = piercing;
            return stats;
        }

        private static EntityStatsVariables MageStats(float fire, float ice)
        {
            var stats = new EntityStatsVariables();
            stats.FireDamage = fire;
            stats.IceDamage = ice;
            return stats;
        }

        private static EntityStatsVariables EnemyStats(float slashing, float blunt, float piercing,
            float fire, float ice, float wind, float earth, float light, float dark)
        {
            var stats = new EntityStatsVariables();
            stats.SlashingDefense = slashing;
            stats.BluntDefense = blunt;
            stats.PiercingDefense = piercing;
            stats.FireDefense = fire;
            stats.IceDefense = ice;
            stats.WindDefense = wind;
            stats.EarthDefense = earth;
            stats.LightDefense = light;
            stats.DarkDefense = dark;
            return stats;
        }

        private class Combatant
        {
            private readonly int _seedHp;
            private readonly int _seedMana;

            public Combatant(string name, int index, int classId, string className, int maxHp, int maxMana, bool isEnemy)
            {
                Name = name;
                Index = index;
                ClassId = classId;
                ClassName = className;
                MaxHp = maxHp;
                CurrentHp = maxHp;
                MaxMana = maxMana;
                CurrentMana = maxMana;
                IsCaster = !isEnemy;
                _seedHp = maxHp;
                _seedMana = maxMana;
            }

            public string Name { get; }
            public int Index { get; }
            public int ClassId { get; }
            public string ClassName { get; }
            public int MaxHp { get; }
            public int MaxMana { get; }
            public int CurrentHp { get; set; }
            public int CurrentMana { get; set; }
            public bool IsCaster { get; }
            public bool IsAlive => CurrentHp > 0;
            public EntityStatsVariables Stats { get; set; } = new();

            public void RestoreFromSeed()
            {
                CurrentHp = _seedHp;
                CurrentMana = _seedMana;
            }
        }
    }
}
