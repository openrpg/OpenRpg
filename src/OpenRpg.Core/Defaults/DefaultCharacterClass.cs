using OpenRpg.Core.Classes;

namespace OpenRpg.Core.Defaults
{
    public class DefaultCharacterClass : IClass
    {
        public int Level { get; }
        public IClassTemplate ClassTemplate { get; }

        public DefaultCharacterClass(int level, IClassTemplate classTemplate)
        {
            Level = level;
            ClassTemplate = classTemplate;
        }
    }
}