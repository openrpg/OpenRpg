using System.Collections.Generic;

namespace OpenRpg.Genres.Fantasy.Player
{
    public interface IPlayerState
    {
        IDictionary<int, bool> Triggers { get; }
        IDictionary<int, int> QuestStates { get; }
    }
}