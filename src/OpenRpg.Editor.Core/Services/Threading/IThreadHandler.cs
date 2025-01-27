using System;
using System.Threading.Tasks;

namespace OpenRpg.Editor.Core.Services.Threading;

public interface IThreadHandler
{
    void For(int start, int end, Action<int> process);
    Task Run(Action process);
}