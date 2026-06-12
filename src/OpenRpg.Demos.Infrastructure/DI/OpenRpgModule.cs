using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Core.Utils;
using OpenRpg.Demos.Infrastructure.Scheduling;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Effects;
using OpenRpg.Genres.Fantasy.Builders;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Equippables.Validators;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Genres.Fantasy.Objectives;
using OpenRpg.Genres.Fantasy.State.Populators;
using OpenRpg.Genres.Fantasy.Stats.Populators;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Genres.Populators.Entity.Stats;
using OpenRpg.Genres.Requirements;
using OpenRpg.Genres.Objectives;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Loot;
using OpenRpg.Tags;
using OpenRpg.Tags.Data;

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
            services.AddSingleton<ILootTableProcessor, DefaultLootTableProcessor>();
            services.AddSingleton<IEntityAttackGenerator, FantasyAttackGenerator>();
            services.AddSingleton<IAttackProcessor<EntityStatsVariables>, DefaultAttackProcessor>();
            services.AddSingleton<ICharacterRequirementChecker, DefaultFantasyCharacterRequirementChecker>();
            services.AddSingleton<ICharacterObjectiveChecker, DefaultFantasyCharacterObjectiveChecker>();
            services.AddSingleton<ICharacterEffectProcessor, CharacterEffectProcessor>();
            services.AddSingleton<ICharacterPopulator, CharacterPopulator>();
            services.AddSingleton<FantasyCharacterBuilder>();
            services.AddSingleton<IEquipmentSlotValidator, FantasyCharacterEquipmentSlotValidator>();
            services.AddSingleton<ITagRegistry>(CreateTagRegistry());
        }

        private static ITagRegistry CreateTagRegistry()
        {
            // Tag IDs used in the demo
            const int Armour = 1, Weapon = 2, Heavy = 3, Light = 4, Metal = 5, Wood = 6, Leather = 7, Fire = 8, Ice = 9, Consumable = 10;

            var registry = new TagRegistry();
            registry.AddRelationship(Armour, Heavy, 0.5f);
            registry.AddRelationship(Armour, Light, 0.5f);
            registry.AddRelationship(Armour, Metal, 0.4f);
            registry.AddRelationship(Armour, Wood, 0.2f);
            registry.AddRelationship(Armour, Leather, 0.6f);

            registry.AddRelationship(Weapon, Heavy, 0.3f);
            registry.AddRelationship(Weapon, Light, 0.7f);
            registry.AddRelationship(Weapon, Metal, 0.8f);
            registry.AddRelationship(Weapon, Wood, 0.4f);

            registry.AddRelationship(Heavy, Metal, 0.6f);
            registry.AddRelationship(Heavy, Wood, 0.3f);
            registry.AddRelationship(Heavy, Leather, -0.3f);

            registry.AddRelationship(Light, Leather, 0.7f);
            registry.AddRelationship(Light, Wood, 0.3f);
            registry.AddRelationship(Light, Metal, -0.2f);

            registry.AddRelationship(Fire, Heavy, -0.3f);
            registry.AddRelationship(Fire, Light, 0.3f);

            registry.AddRelationship(Ice, Heavy, 0.3f);
            registry.AddRelationship(Ice, Light, -0.3f);

            return registry;
        }
    }
}