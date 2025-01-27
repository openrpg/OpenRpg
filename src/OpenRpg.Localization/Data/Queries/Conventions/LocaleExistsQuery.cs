using System;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class LocaleExistsQuery : ILocaleQuery<bool>
    {
        public string Id { get; }

        public LocaleExistsQuery(string id)
        {
            if (string.IsNullOrEmpty(id))
            { throw new ArgumentException("id cannot be null or empty", nameof(id)); }
            
            Id = id;
        }

        public bool Execute(string locale, ILocaleDataSource dataSource)
        { return dataSource.Exists(locale, Id); }
    }
}