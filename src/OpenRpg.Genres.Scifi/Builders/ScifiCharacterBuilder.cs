using OpenRpg.Core.Utils;
using OpenRpg.Genres.Builders;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Builders
{
    public class ScifiCharacterBuilder : CharacterBuilder
    {
        public ScifiCharacterBuilder(IRandomizer randomizer, ICharacterPopulator characterPopulator) : base(randomizer, characterPopulator)
        {
        }

        protected override void PreProcessCharacter()
        {
            _equipment.TryAdd(ScifiEntityEquipmentSlotTypes.ArmourSlot, null);
            _equipment.TryAdd(ScifiEntityEquipmentSlotTypes.WeaponSlot, null);
        }
    }
}