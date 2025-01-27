using System;
using System.Threading.Tasks;

namespace OpenRpg.Editor.Core.Services.Threading;

public class ThreadHandler : IThreadHandler
{
    public void For(int start, int end, Action<int> process) => Parallel.For(start, end, process);
    public Task Run(Action process) => Task.Run(process);
}