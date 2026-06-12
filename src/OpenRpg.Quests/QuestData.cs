using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests
{
    public class QuestData : ITemplateData<QuestDataVariables>
    {
        public int TemplateId { get; set; }
        public int State { get; set; } = QuestStateTypes.QuestNotStarted;
        public ObjectiveState ObjectiveState { get; set; } = new();
        public QuestDataVariables Variables { get; set; } = new();

        public int GetObjectiveProgress(int objectiveIndex)
        {
            var key = MakeObjectiveKey(objectiveIndex);
            return ObjectiveState.ContainsKey(key) ? ObjectiveState[key] : 0;
        }

        public void AddObjectiveProgress(int objectiveIndex, int amount)
        {
            var key = MakeObjectiveKey(objectiveIndex);
            var current = ObjectiveState.ContainsKey(key) ? ObjectiveState[key] : 0;
            ObjectiveState[key] = current + amount;
        }

        public bool IsObjectiveComplete(int objectiveIndex, int requiredAmount)
            => GetObjectiveProgress(objectiveIndex) >= requiredAmount;

        public void ClearObjectives(int objectiveCount)
        {
            for (var i = 0; i < objectiveCount; i++)
            {
                var key = MakeObjectiveKey(i);
                if (ObjectiveState.ContainsKey(key))
                { ObjectiveState.Remove(key); }
            }
        }

        private int MakeObjectiveKey(int objectiveIndex)
            => (TemplateId << 16) | (objectiveIndex & 0xFFFF);
    }
}
