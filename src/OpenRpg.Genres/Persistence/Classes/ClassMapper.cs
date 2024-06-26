using System.Linq;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Variables;

namespace OpenRpg.Genres.Persistence.Classes
{
    public abstract class ClassMapper : IClassMapper
    {
        public IClass Map(ClassData data)
        {
            var classVars = new DefaultClassVariables(data.Variables
                .ToDictionary(x => x.Key, x => x.Value));

            var classTemplate = GetClassTemplateFor(data.ClassTemplateId);

            return InitializeClass(data, classVars, classTemplate);
        }

        public virtual IClass InitializeClass(ClassData data, IClassVariables variables, IClassTemplate template)
        {
            return new DefaultClass
            {
                Variables = variables,
                Template = template,
            };
        }

        public abstract IClassTemplate GetClassTemplateFor(int classTemplateId);
    }
}