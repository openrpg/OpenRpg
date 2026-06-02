using System;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

public class ItemEffectApplier
{
    public record ApplyResult(int HealAmount, int ManaAmount, int ReviveAmount);

    public ApplyResult ApplyItemEffects(ItemData itemData, BattleEntity target, ItemTemplate template)
    {
        var healAmount = 0;
        var manaAmount = 0;
        var reviveAmount = 0;

        if (template.Variables.Effects == null) return new ApplyResult(0, 0, 0);

        foreach (var effect in template.Variables.Effects)
        {
            if (effect is not StaticEffect se) continue;

            if (se.EffectType == GenreEffectTypes.HealthRestoreAmount)
            {
                healAmount = (int)se.Potency;
                var newHp = Math.Min(target.Hp + healAmount, target.MaxHp);
                var actualHeal = newHp - target.Hp;
                target.Hp = newHp;
            }
            else if (se.EffectType == GenreEffectTypes.HealthRestorePercentage)
            {
                healAmount = (int)(target.MaxHp * se.Potency);
                var newHp = Math.Min(target.Hp + healAmount, target.MaxHp);
                var actualHeal = newHp - target.Hp;
                target.Hp = newHp;
            }
            else if (se.EffectType == FantasyEffectTypes.ManaRestoreAmount)
            {
                manaAmount = (int)se.Potency;
                var newMp = (int)Math.Min(target.Mana + manaAmount, target.MaxMana);
                target.Entity.State.Mana = newMp;
            }
            else if (se.EffectType == GenreEffectTypes.LifeRestoreAmount)
            {
                var reviveHp = (int)se.Potency;
                target.Entity.State.RestoreLife(reviveHp, target.MaxHp);
                reviveAmount = target.Hp;
            }
            else if (se.EffectType == GenreEffectTypes.LifeRestorePercentage)
            {
                var reviveHp = (int)(target.MaxHp * se.Potency);
                target.Entity.State.RestoreLife(reviveHp, target.MaxHp);
                reviveAmount = target.Hp;
            }
        }

        return new ApplyResult(healAmount, manaAmount, reviveAmount);
    }
}
