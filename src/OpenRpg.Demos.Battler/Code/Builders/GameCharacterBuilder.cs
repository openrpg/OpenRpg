using OpenRpg.Core.Utils;
using OpenRpg.Data;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Fantasy.Builders;
using OpenRpg.Genres.Populators.Entity;

namespace OpenRpg.Demos.Battler.Code.Builders;

public class GameCharacterBuilder : FantasyCharacterBuilder
{
    private readonly IDataSource _dataSource;

    public GameCharacterBuilder(IRandomizer randomizer, ICharacterPopulator characterPopulator, IDataSource dataSource)
        : base(randomizer, characterPopulator)
    {
        _dataSource = dataSource;
    }

    protected override void PostProcessCharacter(Character character)
    {
        if (character.Variables.HasClass())
        {
            var template = _dataSource.Get<ClassTemplate>(character.Variables.Class.TemplateId);
            if (template?.Variables.HasAssetCode() == true)
                character.Variables.AssetCode = template.Variables.AssetCode;
        }
        base.PostProcessCharacter(character);
    }
}
