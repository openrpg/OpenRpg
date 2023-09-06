using System;
using System.Collections.Generic;
using OpenRpg.Combat.Effects;

namespace OpenRpg.Combat.Processors.Effects
{
    public interface IActiveEffectProcessor
    {
        event EventHandler<ActiveEffect> EffectTriggered;
        event EventHandler<ActiveEffect> EffectExpired;

        void Process(IReadOnlyList<ActiveEffect> activeEffects, float elapsedTime);
    }
}