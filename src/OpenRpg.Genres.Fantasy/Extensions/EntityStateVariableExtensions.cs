using OpenRpg.Core.Extensions;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EntityStateVariableExtensions
    {
        public static int Magic(this EntityStateVariables state) => (int)state.Get(FantasyEntityStateVariableTypes.Magic);
        public static void Magic(this EntityStateVariables state, int value) => state[FantasyEntityStateVariableTypes.Magic] = value;
        
        public static void AddMagic(this EntityStateVariables state, int change, int? maxMagic = null)
        {
            var newValue = state.Magic() + change;
            if (maxMagic.HasValue)
            { state.AddValue(FantasyEntityStateVariableTypes.Magic, newValue, 0, maxMagic.Value); }
            else
            { state.Magic(newValue); }
        }

        public static void DeductMagic(this EntityStateVariables state, int change, int? maxMagic = null)
        {
            var newValue = state.Magic() - change;
            if (maxMagic.HasValue)
            { state.AddValue(FantasyEntityStateVariableTypes.Magic, newValue, 0, maxMagic.Value); }
            else
            { state.Magic(newValue); }
        }
    }
}