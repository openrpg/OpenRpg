namespace OpenRpg.Core.Classes
{
    public class DefaultClass : IClass
    {
        public int Level { get; set; }
        public IClassTemplate ClassTemplate { get; set; }
        public IClassVariables Variables { get; set; } = new DefaultClassVariables();

        public DefaultClass(){}
        
        public DefaultClass(int level, IClassTemplate classTemplate)
        {
            Level = level;
            ClassTemplate = classTemplate;
        }
    }
}