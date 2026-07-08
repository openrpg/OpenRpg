using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Extensions;

namespace OpenRpg.Cards.Genres.Conventions
{
    public abstract class TemplateDataCardWithEffects<T,TTemplateVars> : GenericDataCard<T>
        where T : ITemplate<TTemplateVars> 
        where TTemplateVars : ITemplateVariables
    {
        protected TemplateDataCardWithEffects(T data) : base(data)
        { }
        
        public override IReadOnlyCollection<IEffect> Effects => Data.Variables.Effects;
    }
}