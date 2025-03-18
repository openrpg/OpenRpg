using OpenRpg.Cards.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Cards
{
    /// <summary>
    /// The Card interface provides a way to wrap up underlying objects and express them as playable cards
    /// </summary>
    public interface ICard : IHasLocaleDescription, IHasEffects, IHasVariables<ICardVariables>
    {
        int CardType { get; }
    }
}