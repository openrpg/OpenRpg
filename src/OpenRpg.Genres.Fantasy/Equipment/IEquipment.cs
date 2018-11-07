using OpenRpg.Genres.Fantasy.Equipment.Slots;

namespace OpenRpg.Genres.Fantasy.Equipment
{
    public interface IEquipment
    {
        HeadSlot HeadSlot { get; }
        BackSlot BackSlot { get; }
        UpperBodySlot UpperBodySlot { get; }
        WristSlot WristSlot { get; }
        MainHandSlot MainHandSlot { get; }
        OffHandSlot OffHandSlot { get; }
        LowerBodySlot LowerBodySlot { get; }
        FootSlot FootSlot { get; }
        
        NeckSlot NeckSlot { get; }
        RingSlot Ring1Slot { get; }
        RingSlot Ring2Slot { get; }
    }
}