using System;
using System.Reactive;

namespace OpenRpg.Editor.Core.Services.Modal;

public interface IModalService : IModalInteractionService, IDisposable
{
    public IObservable<ModalReference> OnModalOpenRequest { get; }
    public IObservable<Unit> OnModalCloseRequest { get; }

    public void ClosingModal(string id);
}