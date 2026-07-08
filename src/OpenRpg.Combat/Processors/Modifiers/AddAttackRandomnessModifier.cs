using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Utils;

namespace OpenRpg.Combat.Processors.Modifiers
{
    public class AddAttackRandomnessModifier : IAttackModifier
    {
        public IRandomizer Randomizer { get; }

        public bool ShouldApply(Attack attack) => true;
        
        public AddAttackRandomnessModifier(IRandomizer randomizer)
        { Randomizer = randomizer; }

        public float GenerateRandomFrom(float maximumValue, float startFrom = 0.75f)
        { return Randomizer.Random(maximumValue * startFrom, maximumValue); }

        public Attack ModifyValue(Attack attack)
        {
            var damages = new List<Damage>();
            foreach (var damage in attack.Damages)
            {
                var newValue = GenerateRandomFrom(damage.Value);
                if(newValue >= 0)
                { damages.Add(damage with { Value = newValue }); }
            }
            return attack with { Damages = damages };
        }
    }
}