using OpenRpg.Core.Common;

namespace OpenRpg.Quests
{
    public class Objective : IHasAssociation
    {
        public int ObjectiveType { get; set; }
        public int AssociatedId { get; set; }
        public int AssociatedValue { get; set; }
    }
}