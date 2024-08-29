using System.Collections.Generic;
using OpenRpg.Core.Templates;

namespace OpenRpg.Data
{
    public class DefaultTemplateAccessor : ITemplateAccessor
    {
        public IDataSource DataSource { get; }

        public DefaultTemplateAccessor(IDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public T Get<T>(int templateId) where T : ITemplate => DataSource.Get<T>(templateId);
        public IEnumerable<T> GetAll<T>() where T : ITemplate => DataSource.GetAll<T>();
    }
}

