using System;

namespace OpenRpg.Editor.Core.Services.Modal;

public interface IModalInteractionService
{
    public IObservable<string> OnModalClosed { get; }
    
    public void ShowModal(ModalReference reference);
    public void CloseModal();
}