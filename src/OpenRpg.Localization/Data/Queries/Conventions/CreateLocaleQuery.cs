using System;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class CreateLocaleQuery : ILocaleQuery<string>
    {
        public string Text { get; }
        public string Id { get; }

        public CreateLocaleQuery(string id, string text)
        {
            if (string.IsNullOrEmpty(id))
            { throw new ArgumentException("id cannot be null or empty", nameof(id)); }
            
            Text = text;
            Id = id;
        }

        public string Execute(string localeCode, ILocaleDataSource dataSource)
        {
            dataSource.Create(localeCode, Id, Text);
            return Id;
        }
    }
}