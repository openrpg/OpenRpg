using System;
using System.Collections.Generic;
using OpenRpg.Entities.Effects;
using Range = OpenRpg.Core.Utils.Range;

namespace OpenRpg.Entities.Procedural
{
    public class ProceduralEffects
    {
        public Range EffectAmount { get; set; }
        public IReadOnlyList<GroupedEffect> Effects { get; set; } = Array.Empty<GroupedEffect>();
    }
}