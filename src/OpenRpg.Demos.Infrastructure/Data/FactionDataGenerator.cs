using System.Collections.Generic;
using OpenRpg.Quests.Factions;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class FactionDataGenerator : IDataGenerator<DefaultFaction>
    {
        public const int MerchantsGuildId = 1;
        public const int CityGuardId = 2;
        public const int ThievesGuildId = 3;

        public IEnumerable<DefaultFaction> GenerateData()
        {
            return new[]
            {
                MakeMerchantsGuild(),
                MakeCityGuard(),
                MakeThievesGuild()
            };
        }

        private DefaultFaction MakeMerchantsGuild()
        {
            return new DefaultFaction
            {
                Id = MerchantsGuildId,
                NameLocaleId = "Merchants Guild",
                DescriptionLocaleId = "A powerful trade organisation that controls most commerce in the region. They value fair dealing and reliable trade partners."
            };
        }

        private DefaultFaction MakeCityGuard()
        {
            return new DefaultFaction
            {
                Id = CityGuardId,
                NameLocaleId = "City Guard",
                DescriptionLocaleId = "The official law enforcement arm of the city, maintaining order and safety. They respect those who uphold the law."
            };
        }

        private DefaultFaction MakeThievesGuild()
        {
            return new DefaultFaction
            {
                Id = ThievesGuildId,
                NameLocaleId = "Thieves Guild",
                DescriptionLocaleId = "A shadowy network of rogues and smugglers operating in the city's underbelth. Trust is earned through discrete transactions."
            };
        }
    }
}
