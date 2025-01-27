using System;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class GetLocaleQuery : ILocaleQuery<string>
    {
        public string Id { get; }

        public GetLocaleQuery(string id)
        {
            if (string.IsNullOrEmpty(id))
            { throw new ArgumentException("id cannot be null or empty", nameof(id)); }
            
            Id = id;
        }

        public string Execute(string locale, ILocaleDataSource dataSource)
        { return dataSource.Get(locale, Id); }
    }
}