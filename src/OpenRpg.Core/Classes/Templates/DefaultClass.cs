using OpenRpg.Core.Templates;

namespace OpenRpg.Core.Classes.Templates
{
    public class DefaultClass : IClass
    {
        public IClassTemplateInstance Instance { get; set; } = new DefaultClassTemplateInstance();
        public IClassTemplate Template { get; set; } = new DefaultClassTemplate();
    }
}