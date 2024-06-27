using System;
using OpenRpg.Core.Classes.Variables;

namespace OpenRpg.Core.Classes.Templates
{
    public class DefaultClassTemplateInstance : IClassTemplateInstance
    {
        public Guid UniqueId { get; set; }
        public int TemplateId { get; set; }
        public IClassVariables Variables { get; set; } = new DefaultClassVariables();
    }
}