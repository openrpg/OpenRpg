using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class UpdateLocaleQuery : ILocaleQuery<string>
    {
        public string Text { get; }
        public string Key { get; }

        public UpdateLocaleQuery(string text, string key)
        {
            Text = text;
            Key = key;
        }

        public string Execute(string locale, ILocaleDataSource dataSource)
        {
            dataSource.Update(locale, Text, Key);
            return Key;
        }
    }
}