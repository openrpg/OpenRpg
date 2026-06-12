# `OpenRpg.Quests`

The quests project builds upon the `OpenRpg.Items` project to allow a way to express quests that can be carried out by players within the game.

A `QuestTemplate` is a `Template` that generally contains information about the `Objectives`, the `Rewards` as well as other metadata and a way to track the state of completed quests and faction information.

> It is recommended you look over the interactive web demo for a more in depth example and explanation of this project.

## Quest Structure

```csharp
var quest = new QuestTemplate
{
    Id = 1,
    NameLocaleId = "Goblin Hunt",
    DescriptionLocaleId = "Defeat 3 goblins terrorising the village.",
    IsRepeatable = false,
    Objectives = new List<Objective> { ... },
    Rewards = new List<Reward> { ... },
    Gifts = new List<Reward> { ... },           // items given at quest start
    Variables = new QuestTemplateVariables()             // stores Requirements at key 4003
};
```

### Objectives

Each `Objective` has an `ObjectiveType` (int) and an `Association` (AssociatedId + AssociatedValue).

| Type | Constant | Meaning |
|------|----------|---------|
| 1 | `TriggerObjective` | A trigger must be set |
| 2 | `ItemObjective` | Collect N items (AssociatedId = item template id, AssociatedValue = count) |
| 3 | `LevelObjective` | Reach a level (AssociatedValue = required level) |
| 4 | `ClassObjective` | Have a class (AssociatedId = class template id) |
| 5 | `EffectObjective` | Have an active effect (AssociatedId = effect id) |
| 6 | `QuestObjective` | Complete another quest (AssociatedId = quest id) |
| 30 | `CurrencyObjective` | Collect N currency (AssociatedValue = amount) |
| 31 | `EnemyDefeatedObjective` | Defeat N enemies (AssociatedId = enemy id, AssociatedValue = count) |
| 32 | `EnemySightedObjective` | Sight N enemies (AssociatedId = enemy id, AssociatedValue = count) |

### Rewards

Rewards have a `RewardType`, `RewardChance`, and `Association`. Common types include experience, currency, and item rewards.

### Requirements

Requirements live on `quest.Variables.Requirements` (key 4003) and gate quest acceptance. They are checked via `ICharacterRequirementChecker`.

## Objective Verification

The `ICharacterObjectiveChecker` follows the same pattern as `ICharacterRequirementChecker` — it has four overloads for different contexts:

```csharp
public interface ICharacterObjectiveChecker : IObjectiveChecker<Character>
{
    // Stateless checks — verified from live character state
    bool IsObjectiveMet(Character character, Objective objective);         // level, class, effects
    bool IsObjectiveMet(IQuestState state, Objective objective);          // quest prerequisites
    bool IsObjectiveMet(ITriggerState state, Objective objective);        // trigger objectives

    // Stateful checks — verified from accumulated progress
    bool IsObjectiveMet(IObjectiveState state, Objective objective, int questId, int objectiveIndex);
}
```

**Stateless objectives** (trigger, quest, level, class, effect) are checked against live game state each time.

**Stateful objectives** (items, kills, currency) compare accumulated progress against the required threshold.

### Batch Checking

```csharp
// Check all objectives for a quest across all four contexts
var allMet = ObjectiveChecker.AreObjectivesMet(character, questData);
```

## Objective State Tracking

`IObjectiveState` stores per-quest-per-objective progress as a dictionary of composite keys to int counts.

The composite key combines `questId` and `objectiveIndex` into a single int: `(questId << 16) | objectiveIndex`.

```csharp
// Access via entity variables (optional — can also use standalone)
character.Variables.ObjectiveState.GetObjectiveProgress(questId, objectiveIndex);
character.Variables.ObjectiveState.AddObjectiveProgress(questId, objectiveIndex, 1);
character.Variables.ObjectiveState.IsObjectiveComplete(questId, objectiveIndex, requiredAmount);
character.Variables.ObjectiveState.ClearQuestObjectives(questId, quest.Objectives.Count);
```

Progress is **push-based** — game code explicitly calls `AddObjectiveProgress` when events happen (enemy killed, item collected, etc.).

## QuestData

`QuestData` is a proper `ITemplateData` instance that bundles quest definition, state, and objective state. It links to the template via `TemplateId` (like `ItemData`):

```csharp
var questData = new QuestData(quest.Id, QuestStateTypes.QuestActive);
// questData.TemplateId    — links to the QuestTemplate
// questData.State         — current state (NotStarted/Active/Complete)
// questData.ObjectiveState — per-objective progress
```

## Typical Usage

```csharp
// Accept a quest
var questData = new QuestData(quest.Id, QuestStateTypes.QuestActive);
questData.ObjectiveState.ClearQuestObjectives(quest.Id, quest.Objectives.Count);

// Track progress (called by game code when events happen)
questData.ObjectiveState.AddObjectiveProgress(quest.Id, objectiveIndex, 1);

// Check if all objectives are met
var canComplete = ObjectiveChecker.AreObjectivesMet(character, quest, questData);

// Complete the quest
questData.State = QuestStateTypes.QuestComplete;
```
