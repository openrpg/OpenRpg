using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Genres.Characters;

namespace OpenRpg.Genres.Populators.Entity
{
    public class CharacterPopulator : EntityPopulator<Character>, ICharacterPopulator
    {
        public CharacterPopulator(IEntityStatPopulator statPopulator, IEntityStatePopulator statePopulator, IEffectProcessor<Character> effectProcessor) : base(statPopulator, statePopulator, effectProcessor)
        {
        }
    }
}