using System.Collections.Generic;
using OpenRpg.Core.Templates;

namespace OpenRpg.Data
{
    public class TemplateAccessor : ITemplateAccessor
    {
        public IDataSource DataSource { get; }

        public TemplateAccessor(IDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public T Get<T>(int templateId) where T : ITemplate => DataSource.Get<T>(templateId);
        public IEnumerable<T> GetAll<T>() where T : ITemplate => DataSource.GetAll<T>();
    }
}

