using System.Collections.Generic;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Types;
using OpenRpg.Quests;
using OpenRpg.Quests.Types;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class QuestStateDataGenerator : IDataGenerator<QuestTemplate>
    {
        public const int FindArtifactQuestId = 10;
        public const int DefeatGoblinQuestId = 20;
        public const int DeliverLetterQuestId = 30;

        public const int SpokenToElderTriggerId = 1;
        public const int FoundMapTriggerId = 2;

        public IEnumerable<QuestTemplate> GenerateData()
        {
            return new[]
            {
                MakeFindArtifactQuest(),
                MakeDefeatGoblinQuest(),
                MakeDeliverLetterQuest()
            };
        }

        private QuestTemplate MakeFindArtifactQuest()
        {
            var quest = new QuestTemplate
            {
                Id = FindArtifactQuestId,
                NameLocaleId = "Find the Lost Artifact",
                DescriptionLocaleId = "The Elder knows of an ancient artifact hidden in the ruins. Speak to him first to learn its location.",
                IsRepeatable = false,
                Objectives = new List<Objective>
                {
                    new() { ObjectiveType = ObjectiveTypes.TriggerObjective, Association = new Association(FoundMapTriggerId, 1) }
                },
                Rewards = new List<Reward>
                {
                    new() { RewardType = FantasyRewardTypes.ExperienceReward, Association = new Association(0, 200) },
                    new() { RewardType = GenreRewardTypes.CurrencyReward, Association = new Association(0, 150) }
                },
                Variables = new Quests.Variables.QuestTemplateVariables()
            };
            quest.Variables.Requirements = new[]
            {
                new OpenRpg.Core.Requirements.Requirement
                {
                    RequirementType = QuestRequirementTypes.TriggerRequirement,
                    Association = new Association(SpokenToElderTriggerId, 1)
                }
            };
            return quest;
        }

        private QuestTemplate MakeDefeatGoblinQuest()
        {
            var quest = new QuestTemplate
            {
                Id = DefeatGoblinQuestId,
                NameLocaleId = "Defeat the Goblin Chief",
                DescriptionLocaleId = "The goblins have been raiding caravans. Slay their chief to end the threat.",
                IsRepeatable = false,
                Objectives = new List<Objective>
                {
                    new() { ObjectiveType = GenresObjectiveTypes.EnemyDefeatedObjective, Association = new Association(99, 3) }
                },
                Rewards = new List<Reward>
                {
                    new() { RewardType = FantasyRewardTypes.ExperienceReward, Association = new Association(0, 300) },
                    new() { RewardType = GenreRewardTypes.ItemReward, Association = new Association(ItemTemplateLookups.SuperSword, 1) }
                },
                Variables = new Quests.Variables.QuestTemplateVariables()
            };
            return quest;
        }

        private QuestTemplate MakeDeliverLetterQuest()
        {
            var quest = new QuestTemplate
            {
                Id = DeliverLetterQuestId,
                NameLocaleId = "Deliver the Letter",
                DescriptionLocaleId = "A simple errand — deliver a sealed letter to the merchant on the other side of town.",
                IsRepeatable = true,
                Objectives = new List<Objective>
                {
                    new() { ObjectiveType = ObjectiveTypes.ItemObjective, Association = new Association(ItemTemplateLookups.Chest, 1) }
                },
                Rewards = new List<Reward>
                {
                    new() { RewardType = FantasyRewardTypes.ExperienceReward, Association = new Association(0, 50) },
                    new() { RewardType = GenreRewardTypes.CurrencyReward, Association = new Association(0, 25) }
                },
                Variables = new Quests.Variables.QuestTemplateVariables()
            };
            return quest;
        }
    }
}
