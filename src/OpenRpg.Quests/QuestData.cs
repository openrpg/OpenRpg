using OpenRpg.Quests.State;

namespace OpenRpg.Quests
{
    public class QuestData
    {
        public const int DefaultState = 1; // QuestStateTypes.QuestNotStarted

        public Quest Quest { get; }
        public int State { get; set; }
        public IObjectiveState ObjectiveState { get; }

        public QuestData(Quest quest, int state = DefaultState, IObjectiveState objectiveState = null)
        {
            Quest = quest;
            State = state;
            ObjectiveState = objectiveState ?? new ObjectiveState();
        }
    }
}
