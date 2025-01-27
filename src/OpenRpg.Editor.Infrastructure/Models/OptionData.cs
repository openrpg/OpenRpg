namespace OpenRpg.Editor.Infrastructure.Models
{
    public class OptionData
    {
        public int Id { get; }
        public string Name { get; }

        public OptionData(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}