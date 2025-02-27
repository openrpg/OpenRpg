using System.Collections.Generic;
using OpenRpg.Cards.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Cards.Genres.Conventions
{
    public abstract class GenericDataCard<T> : ICard
        where T : IHasLocaleDescription
    {
        public abstract int CardType { get; }
        public ICardVariables Variables { get; set; } = new DefaultCardVariables();

        public T Data { get; }

        protected GenericDataCard(T data)
        { Data = data; }

        public virtual string NameLocaleId => Data.NameLocaleId;
        public virtual string DescriptionLocaleId => Data.DescriptionLocaleId;
        public abstract IReadOnlyCollection<IEffect> Effects { get; }
        
    }
}