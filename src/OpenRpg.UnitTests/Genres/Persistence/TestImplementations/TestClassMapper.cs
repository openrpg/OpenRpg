using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Templates;
using OpenRpg.Genres.Persistence.Classes;

namespace OpenRpg.UnitTests.Genres.Persistence.TestImplementations
{
    public class TestClassMapper : ClassMapper
    {
        private readonly DefaultClassTemplate DefaultClassTemplate = new DefaultClassTemplate();
        
        public override IClassTemplate GetClassTemplateFor(int classTemplateId)
        { return new DefaultClassTemplate(); }
    }
}