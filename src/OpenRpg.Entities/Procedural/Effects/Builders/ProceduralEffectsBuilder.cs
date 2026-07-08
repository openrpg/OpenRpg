using System.Collections.Generic;
using OpenRpg.Core.Utils;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Procedural.Effects.Builders;

public class ProceduralEffectsBuilder
{
    protected Range EffectAmount = new(1, 1);
    protected List<GroupedEffect> Effects = new();

    public static ProceduralEffectsBuilder Create()
    { return new ProceduralEffectsBuilder(); }

    public ProceduralEffectsBuilder WithPrimaryEffect(ScaledEffect effect)
    {
        var primaryEffect = new GroupedEffect
        {
            GroupType = CoreProceduralGroupTypes.Primary,
            ScalingIndex = effect.ScalingIndex,
            EffectType = effect.EffectType,
            ScalingType = effect.ScalingType,
            PotencyFunction = effect.PotencyFunction,
            Requirements = effect.Requirements
        };
        
        Effects.Add(primaryEffect);
        return this;
    }
    
    public ProceduralEffectsBuilder WithOptionalEffect(ScaledEffect effect)
    {
        var primaryEffect = new GroupedEffect
        {
            GroupType = CoreProceduralGroupTypes.Optional,
            ScalingIndex = effect.ScalingIndex,
            EffectType = effect.EffectType,
            ScalingType = effect.ScalingType,
            PotencyFunction = effect.PotencyFunction,
            Requirements = effect.Requirements
        };
        
        Effects.Add(primaryEffect);
        return this;
    }
        
    public ProceduralEffectsBuilder WithEffect(int groupType, ScaledEffect effect)
    {
        var primaryEffect = new GroupedEffect
        {
            GroupType = groupType,
            ScalingIndex = effect.ScalingIndex,
            EffectType = effect.EffectType,
            ScalingType = effect.ScalingType,
            PotencyFunction = effect.PotencyFunction,
            Requirements = effect.Requirements
        };
        
        Effects.Add(primaryEffect);
        return this;
    }

    public ProceduralEffectsBuilder WithEffectAmount(Range minMax)
    {
        EffectAmount = minMax;
        return this;
    }
    
    public ProceduralEffectsBuilder WithEffectAmount(int min, int max)
    {
        EffectAmount = new Range(min, max);
        return this;
    }

    public ProceduralEffects Build()
    {
        return new ProceduralEffects()
        {
            Effects = Effects,
            EffectAmount = EffectAmount
        };
    }
}