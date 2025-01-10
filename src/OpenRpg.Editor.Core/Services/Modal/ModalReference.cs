using Microsoft.AspNetCore.Components;

namespace OpenRpg.Editor.Core.Services.Modal;

public class ModalReference
{
    public string Id { get; set; }
    public RenderFragment ModalContent { get; set; }

    public ModalReference(string id, RenderFragment modalContent)
    {
        Id = id;
        ModalContent = modalContent;
    }
}