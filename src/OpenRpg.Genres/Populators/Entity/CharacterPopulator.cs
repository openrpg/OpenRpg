using OpenRpg.Entities.Entity.Populators;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Effects;

namespace OpenRpg.Genres.Populators.Entity
{
    public class CharacterPopulator : EntityPopulator<Character>, ICharacterPopulator
    {
        public CharacterPopulator(IEntityStatPopulator statPopulator, IEntityStatePopulator statePopulator, ICharacterEffectProcessor effectProcessor) : base(statPopulator, statePopulator, effectProcessor)
        {
        }
    }
}