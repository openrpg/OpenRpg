using System.Collections.Generic;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Demos.Infrastructure.Data
{
    /// <summary>
    /// Mirrors the Battler demo's 10-ability roster (Slash, Power Strike, Chi Blast,
    /// Focus Strike, Backstab, Poison Blade, Fire Bolt, Ice Storm, Cure, Cura) so
    /// the web demo can show every <c>TargetType</c> and a class-gated requirement
    /// in the ability execution sandbox.
    /// </summary>
    public class AbilityTemplateDataGenerator : IDataGenerator<AbilityTemplate>
    {
        public const int Slash = 1;
        public const int PowerStrike = 2;
        public const int ChiBlast = 3;
        public const int FocusStrike = 4;
        public const int Backstab = 5;
        public const int PoisonBlade = 6;
        public const int FireBolt = 7;
        public const int IceStorm = 8;
        public const int Cure = 9;
        public const int Cura = 10;

        public IEnumerable<AbilityTemplate> GenerateData()
        {
            return new[]
            {
                MakeSlash(),
                MakePowerStrike(),
                MakeChiBlast(),
                MakeFocusStrike(),
                MakeBackstab(),
                MakePoisonBlade(),
                MakeFireBolt(),
                MakeIceStorm(),
                MakeCure(),
                MakeCura(),
            };
        }

        public AbilityTemplate MakeSlash()
        {
            var template = NewFighterAbility(Slash, "ability-slash", "ability-slash-desc",
                new Damage(FantasyDamageTypes.SlashingDamage, 10f),
                targetType: CombatTargetTypes.MultipleTarget, targetCount: 3, manaCost: 4);
            template.Variables.AssetCode = "ability-slash";
            return template;
        }

        public AbilityTemplate MakePowerStrike()
        {
            var template = NewFighterAbility(PowerStrike, "ability-power-strike", "ability-power-strike-desc",
                new Damage(FantasyDamageTypes.SlashingDamage, 22f),
                targetType: CombatTargetTypes.SingleTarget, targetCount: 1, manaCost: 8);
            template.Variables.AssetCode = "ability-power-strike";
            return template;
        }

        public AbilityTemplate MakeChiBlast()
        {
            var template = NewMageAbility(ChiBlast, "ability-chi-blast", "ability-chi-blast-desc",
                new Damage(FantasyDamageTypes.BluntDamage, 8f),
                targetType: CombatTargetTypes.MultipleTarget, targetCount: 6, manaCost: 5);
            template.Variables.AssetCode = "ability-chi-blast";
            return template;
        }

        public AbilityTemplate MakeFocusStrike()
        {
            var template = NewMageAbility(FocusStrike, "ability-focus-strike", "ability-focus-strike-desc",
                new Damage(FantasyDamageTypes.BluntDamage, 28f),
                targetType: CombatTargetTypes.SingleTarget, targetCount: 1, manaCost: 10);
            template.Variables.AssetCode = "ability-focus-strike";
            return template;
        }

        public AbilityTemplate MakeBackstab()
        {
            var template = NewFighterAbility(Backstab, "ability-backstab", "ability-backstab-desc",
                new Damage(FantasyDamageTypes.PiercingDamage, 25f),
                targetType: CombatTargetTypes.SingleTarget, targetCount: 1, manaCost: 4);
            template.Variables.AssetCode = "ability-backstab";
            return template;
        }

        public AbilityTemplate MakePoisonBlade()
        {
            var template = NewFighterAbility(PoisonBlade, "ability-poison-blade", "ability-poison-blade-desc",
                new Damage(FantasyDamageTypes.PiercingDamage, 12f),
                targetType: CombatTargetTypes.SingleTarget, targetCount: 1, manaCost: 3);
            template.Variables.AssetCode = "ability-poison-blade";
            return template;
        }

        public AbilityTemplate MakeFireBolt()
        {
            var template = NewMageAbility(FireBolt, "ability-fire-bolt", "ability-fire-bolt-desc",
                new Damage(FantasyDamageTypes.FireDamage, 22f),
                targetType: CombatTargetTypes.SingleTarget, targetCount: 1, manaCost: 15);
            template.Variables.AssetCode = "ability-fire-bolt";
            return template;
        }

        public AbilityTemplate MakeIceStorm()
        {
            var template = NewMageAbility(IceStorm, "ability-ice-storm", "ability-ice-storm-desc",
                new Damage(FantasyDamageTypes.IceDamage, 12f),
                targetType: CombatTargetTypes.MultipleTarget, targetCount: 6, manaCost: 22);
            template.Variables.AssetCode = "ability-ice-storm";
            return template;
        }

        public AbilityTemplate MakeCure()
        {
            var template = NewHealingAbility(Cure, "ability-cure", "ability-cure-desc",
                new Damage(FantasyDamageTypes.LightDamage, 45f),
                targetType: CombatTargetTypes.SingleTarget, targetCount: 1, manaCost: 8);
            template.Variables.AssetCode = "ability-cure";
            return template;
        }

        public AbilityTemplate MakeCura()
        {
            var template = NewHealingAbility(Cura, "ability-cura", "ability-cura-desc",
                new Damage(FantasyDamageTypes.LightDamage, 18f),
                targetType: CombatTargetTypes.MultipleTarget, targetCount: 4, manaCost: 14);
            template.Variables.AssetCode = "ability-cura";
            return template;
        }

        private static AbilityTemplate NewFighterAbility(int id, string nameLocaleId, string descLocaleId,
            Damage damage, int targetType, int targetCount, int manaCost)
        {
            return NewClassGatedAbility(id, nameLocaleId, descLocaleId, damage, targetType, targetCount, manaCost,
                ClassTypeLookups.Fighter);
        }

        private static AbilityTemplate NewMageAbility(int id, string nameLocaleId, string descLocaleId,
            Damage damage, int targetType, int targetCount, int manaCost)
        {
            return NewClassGatedAbility(id, nameLocaleId, descLocaleId, damage, targetType, targetCount, manaCost,
                ClassTypeLookups.Mage);
        }

        private static AbilityTemplate NewHealingAbility(int id, string nameLocaleId, string descLocaleId,
            Damage damage, int targetType, int targetCount, int manaCost)
        {
            return NewClassGatedAbility(id, nameLocaleId, descLocaleId, damage, targetType, targetCount, manaCost,
                ClassTypeLookups.Mage);
        }

        private static AbilityTemplate NewClassGatedAbility(int id, string nameLocaleId, string descLocaleId,
            Damage damage, int targetType, int targetCount, int manaCost, int requiredClassId)
        {
            var template = new AbilityTemplate
            {
                Id = id,
                NameLocaleId = nameLocaleId,
                DescriptionLocaleId = descLocaleId,
            };
            template.Variables.Damage = damage;
            template.Variables.Cooldown = 1.0f;
            template.Variables.Range = 7.0f;
            template.Variables.AttackSize = 2.0f;
            template.Variables.TargetType = targetType;
            template.Variables.TargetCount = targetCount;
            template.Variables.ManaCost = manaCost;
            template.Variables.Requirements = new[]
            {
                new Requirement
                {
                    RequirementType = CoreRequirementTypes.ClassRequirement,
                    Association = new Association(requiredClassId, 0)
                }
            };
            return template;
        }
    }
}
