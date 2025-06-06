﻿using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Cards.Genres.Conventions
{
    public abstract class GenericDataCardWithEffects<T> : GenericDataCard<T>
        where T : IHasLocaleDescription, IHasEffects
    {
        protected GenericDataCardWithEffects(T data) : base(data)
        { }
        
        public override IReadOnlyCollection<IEffect> Effects => Data.Effects;
    }
}