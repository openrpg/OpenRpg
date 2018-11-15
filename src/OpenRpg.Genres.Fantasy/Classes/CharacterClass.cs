using OpenRpg.Core.Classes;

namespace OpenRpg.Genres.Fantasy.Classes
{
    public class CharacterClass : IClass
    {
        public int Level { get; set; }
        public IClassTemplate ClassTemplate { get; set; }
    }
}