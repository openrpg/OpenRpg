using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes
{
    public class Class : IHasTemplateLink, IHasVariables<ClassVariables>
    {
        public int TemplateId { get; set; }
        public ClassVariables Variables { get; set; } = new ClassVariables();
    }
}