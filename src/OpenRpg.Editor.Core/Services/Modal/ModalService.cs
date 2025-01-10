using System;
using System.Reactive;
using System.Reactive.Subjects;

namespace OpenRpg.Editor.Core.Services.Modal;

public class ModalService : IModalService
{
    public Subject<ModalReference> ModalOpenRequest = new();
    public Subject<Unit> ModalCloseRequest = new();
    public Subject<string> ModalClosed = new();

    public IObservable<ModalReference> OnModalOpenRequest => ModalOpenRequest;
    public IObservable<Unit> OnModalCloseRequest => ModalCloseRequest;
    public IObservable<string> OnModalClosed => ModalClosed;
    
    public void ShowModal(ModalReference reference)
    { ModalOpenRequest.OnNext(reference); }

    public void CloseModal()
    { ModalCloseRequest.OnNext(Unit.Default); }

    public void ClosingModal(string id)
    { ModalClosed.OnNext(id); }

    public void Dispose()
    {
        ModalOpenRequest.Dispose();
        ModalCloseRequest.Dispose();
        ModalClosed.Dispose();
    }
}