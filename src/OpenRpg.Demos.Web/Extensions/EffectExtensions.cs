using System.Reflection;
using OpenRpg.Core.Effects;
using OpenRpg.Entities.Effects;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Demos.Web.Extensions;

public static class EffectExtensions
{
    public static int[] PercentageEffectTypeCache = GetAllPercentageEffectTypeIds(typeof(FantasyEffectTypes));
    
    public static string GetPotencySymbol(float potency)
    { return potency > 0 ? "+" : "-"; }
    
    public static string GeneratePotencyText(this IEffect effect)
    {
        if (effect is StaticEffect staticEffect)
        {
            var potencySymbol = GetPotencySymbol(staticEffect.Potency);
            var potencySuffix = IsPercentageEffect(effect.EffectType) ? "%" : "";
            return $"{potencySymbol}{staticEffect.Potency:0}{potencySuffix}";
        }

        if (effect is ScaledEffect scaledEffect)
        {
            var potencySymbol = GetPotencySymbol(scaledEffect.PotencyFunction.OutputScale.Min);
            var potencySuffix = IsPercentageEffect(effect.EffectType) ? "%" : "";
            return $"{potencySymbol}{scaledEffect.PotencyFunction.OutputScale.Min:0}-{scaledEffect.PotencyFunction.OutputScale.Max:0}{potencySuffix}";
        }

        return "?";
    }
    
    private static int[] GetAllPercentageEffectTypeIds(Type effectTypes)
    {
        return effectTypes
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.Name.Contains("Percentage"))
            .Select(x => (int)x.GetValue(null))
            .ToArray();
    }

    public static bool IsPercentageEffect(int effectType)
    { return PercentageEffectTypeCache.Contains(effectType); }

    public static bool IsPositiveEffect(this IEffect effect)
    {
        if (effect is StaticEffect staticEffect)
        { return staticEffect.Potency >= 0; }
        
        if(effect is ScaledEffect scaledEffect)
        { return scaledEffect.PotencyFunction.OutputScale.Min >= 0; }

        return true;
    }
}