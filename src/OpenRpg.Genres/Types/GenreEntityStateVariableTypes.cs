using OpenRpg.Entities.Types;

namespace OpenRpg.Genres.Types
{
    public interface GenreEntityStateVariableTypes : CoreEntityStateVariableTypes
    {
        public static readonly int Health = 1;
        public static readonly int Stamina = 2;
    }
}