using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Cards.Genres.Conventions
{
    public abstract class GenericDataCardWithEffects<T> : GenericDataCard<T>
        where T : IHasLocaleDescription, IHasEffects
    {
        protected GenericDataCardWithEffects(T data) : base(data)
        { }
        
        public override IEnumerable<Effect> Effects => Data.Effects;
    }
}