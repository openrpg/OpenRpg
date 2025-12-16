using System;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Core.Utils;
using OpenRpg.Demos.Infrastructure.Scheduling;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Effects;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Genres.Fantasy.State;
using OpenRpg.Genres.Fantasy.State.Populators;
using OpenRpg.Genres.Fantasy.Stats;
using OpenRpg.Genres.Fantasy.Stats.Populators;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Genres.Populators.Entity.Stats;
using OpenRpg.Genres.Requirements;

namespace OpenRpg.Demos.Infrastructure.DI
{
    public class OpenRpgModule : IModule
    {
        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<IUpdateScheduler, DefaultUpdateScheduler>();
         
            services.AddSingleton<IEntityStatPopulator>(new FantasyStatsPopulator([new DamageStatPopulator(), new DefenseStatPopulator()]));
            services.AddSingleton<IEntityStatePopulator, FantasyStatePopulator>();
            services.AddSingleton<IRandomizer>(x => new DefaultRandomizer(new Random()));
            services.AddSingleton<IEntityAttackGenerator, FantasyAttackGenerator>();
            services.AddSingleton<IAttackProcessor<EntityStatsVariables>, DefaultAttackProcessor>();
            services.AddSingleton<ICharacterRequirementChecker, DefaultFantasyCharacterRequirementChecker>();
            services.AddSingleton<ICharacterEffectProcessor, CharacterEffectProcessor>();
            services.AddSingleton<ICharacterPopulator, CharacterPopulator>();
        }
    }
}