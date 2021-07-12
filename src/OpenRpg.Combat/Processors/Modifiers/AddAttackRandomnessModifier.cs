using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Utils;

namespace OpenRpg.Genres.Fantasy.Combat.Modifiers
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
            foreach (var damage in attack.Damages)
            { damage.Value = GenerateRandomFrom(damage.Value); }

            return attack;
        }
    }
}