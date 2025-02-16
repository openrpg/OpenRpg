using OpenRpg.Entities.Effects;

namespace OpenRpg.Entities.Extensions
{
    public static class ScaledEffectExtensions
    {
        public static StaticEffect Compute(this ScaledEffect scaledEffect, float scalingValue)
        {
            return new StaticEffect
            {
                EffectType = scaledEffect.EffectType,
                Potency = scaledEffect.PotencyFunction.Plot(scalingValue),
                Requirements = scaledEffect.Requirements,
            };
        }
    }
}