using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Cards
{
    /// <summary>
    /// The Card interface provides a way to wrap up underlying objects and express them as playable cards
    /// </summary>
    public interface ICard : IHasLocaleDescription, IHasEffects
    {
        int CardType { get; }
        ICardVariables Variables { get; }
    }
}