using Microsoft.AspNetCore.Components;
using OpenRpg.Editor.Core.Services.Modal;

namespace OpenRpg.Editor.Core.Components.Modal;

public abstract class ModalElementReference : ComponentBase
{
    public abstract ModalReference Reference { get; protected set; }
}