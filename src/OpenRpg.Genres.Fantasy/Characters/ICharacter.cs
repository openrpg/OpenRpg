using OpenRpg.Core.Classes;
using OpenRpg.Core.Common;
using OpenRpg.Core.Races;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Equipment;
using OpenRpg.Genres.Fantasy.Stats;

namespace OpenRpg.Genres.Fantasy.Characters
{
    public interface ICharacter : IHasDataId, IHasAssetCode, IHasLocaleDescription
    {
        byte GenderType { get; }
        IClass Class { get; }
        IRaceTemplate Race { get; }
        IEquipment Equipment { get; }
        IEntityStats Stats { get; }
    }    
}