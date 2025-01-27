using OpenRpg.Core.Common;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests.Factions
{
    public class DefaultFaction :  IHasDataId, IHasLocaleDescription, IHasVariables<FactionVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; } = string.Empty;
        public string DescriptionLocaleId { get; set; } = string.Empty;
        
        public FactionVariables Variables { get; set; } = new FactionVariables();
    }
}