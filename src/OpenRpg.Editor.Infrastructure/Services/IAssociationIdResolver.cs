using System;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Services;

public record AssociationIdOptions
{
    public OptionData[] Options { get; init; } = Array.Empty<OptionData>();
    public bool Disabled { get; init; }
    public bool IsMapped { get; init; }
}

public interface IAssociationIdResolver
{
    AssociationIdOptions GetOptionsFor(string context, int typeValue);
}

public static class AssociationIdContexts
{
    public const string Requirement = "Requirement";
    public const string Objective = "Objective";
    public const string Reward = "Reward";
}
