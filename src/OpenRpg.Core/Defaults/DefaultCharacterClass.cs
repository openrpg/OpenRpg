using OpenRpg.Core.Classes;

namespace OpenRpg.Core.Defaults
{
    public class DefaultCharacterClass : IClass
    {
        public int Level { get; set; }
        public IClassTemplate ClassTemplate { get; set; }

        public DefaultCharacterClass(){}
        
        public DefaultCharacterClass(int level, IClassTemplate classTemplate)
        {
            Level = level;
            ClassTemplate = classTemplate;
        }
    }
}