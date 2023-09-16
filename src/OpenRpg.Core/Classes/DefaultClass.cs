using OpenRpg.Core.Classes.Variables;

namespace OpenRpg.Core.Classes
{
    public class DefaultClass : IClass
    {
        public IClassTemplate Template { get; set; } = new DefaultClassTemplate();
        public IClassVariables Variables { get; set; } = new DefaultClassVariables();

        public DefaultClass(){}
        
        public DefaultClass(IClassTemplate template)
        { Template = template; }
    }
}