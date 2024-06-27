﻿using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests
{
    public interface IQuest : ITemplate, IHasRequirements, IHasVariables<IQuestVariables>
    {
        bool IsRepeatable { get; }
        IEnumerable<Objective> Objectives { get; }
        IEnumerable<Reward> Rewards { get; }
        IEnumerable<Reward> Gifts { get; }
    }
}