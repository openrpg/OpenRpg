using System.Collections.Generic;

namespace OpenRpg.Core.Templates
{
    public interface ITemplateAccessor
    {
        public T Get<T>(int templateId) where T : ITemplate;
        public IEnumerable<T> GetAll<T>() where T : ITemplate;
    }
}