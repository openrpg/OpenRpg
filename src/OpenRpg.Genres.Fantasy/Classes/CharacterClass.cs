using OpenRpg.Core.Classes;

namespace OpenRpg.Genres.Fantasy.Classes
{
    public class CharacterClass : IClass
    {
        public int Level { get; }
        public IClassTemplate ClassTemplate { get; }

        public CharacterClass(int level, IClassTemplate classTemplate)
        {
            Level = level;
            ClassTemplate = classTemplate;
        }
    }
}