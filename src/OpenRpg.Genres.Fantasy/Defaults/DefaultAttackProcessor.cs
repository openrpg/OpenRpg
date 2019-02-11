using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Defense;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultAttackProcessor : IAttackProcessor
    {
        public ProcessedAttack ProcessAttack(Attack attack, IDefenseStats defenseStats)
        {
            var applicableDefenses = defenseStats.AsList().Where(x => x.StatValue != 0);
            var damageLookups = attack.Damages.ToDictionary(x => x.Type, x => x.Value);
            var resultingDefenses = new List<Damage>();
            
            foreach (var applicableDefense in applicableDefenses)
            {
                if (!damageLookups.ContainsKey(applicableDefense.StatType)) 
                { continue; }

                float defendedAmount;
                
                if(damageLookups[applicableDefense.StatType] > applicableDefense.StatValue)
                { defendedAmount = applicableDefense.StatValue; }
                else
                { defendedAmount = applicableDefense.StatValue - damageLookups[applicableDefense.StatType]; }
                
                damageLookups[applicableDefense.StatType] -= defendedAmount;
                resultingDefenses.Add(new Damage(applicableDefense.StatType, defendedAmount));
            }

            var resultingDamage = damageLookups.Select(x => new Damage(x.Key, x.Value));
            return new ProcessedAttack(resultingDamage.ToList(), resultingDefenses);
        }
    }
}