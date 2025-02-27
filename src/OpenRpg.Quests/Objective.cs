using OpenRpg.Core.Common;
using OpenRpg.Core.Utils;

namespace OpenRpg.Quests
{
    public class Objective : IHasAssociation
    {
        public int ObjectiveType { get; set; }
        public Association Association { get; set; }
    }
}