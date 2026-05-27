using OpenRpg.Core.Extensions;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EntityStateVariableExtensions
    {
        extension(EntityStateVariables state)
        {
            public float Mana
            {
                get => state.Get(FantasyEntityStateVariableTypes.Mana);
                set => state[FantasyEntityStateVariableTypes.Mana] = value;
            }
        }
        
        public static void AddMana(this EntityStateVariables state, int change, int? maxMana = null)
        {
            if (maxMana.HasValue)
            { state.AddValue(FantasyEntityStateVariableTypes.Mana, change, 0, maxMana.Value); }
            else
            { state.Mana = state.Mana + change; }
        }

        public static void DeductMana(this EntityStateVariables state, int change, int? maxMana = null)
        {
            if (maxMana.HasValue)
            { state.AddValue(FantasyEntityStateVariableTypes.Mana, -change, 0, maxMana.Value); }
            else
            { state.Mana = state.Mana - change; }
        }
    }
}