using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Cards
{
    public interface ICard : IHasLocaleDescription, IHasEffects
    {
        int CardType { get; }
        ICardVariables Variables { get; }
    }
}