using System.Collections.Generic;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Battler.Code.Services.Game;

public interface IPersistentGameState
{
    List<BattleEntity> Party { get; }
    List<ItemData> SharedInventory { get; }
    void InitializeParty();
    bool IsInitialized { get; }
    BattleEntity GetCharacter(int index);
}
