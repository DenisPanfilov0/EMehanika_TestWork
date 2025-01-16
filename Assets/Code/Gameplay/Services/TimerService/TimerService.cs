using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Code.Gameplay.Services.TimerService
{
    public class TimerService : ITimerService
    {
        private readonly ConcurrentDictionary<Guid, CancellationTokenSource> _timers = new();
        private readonly SynchronizationContext _mainThreadContext = SynchronizationContext.Current;

        public Guid StartTimer(float duration, Action callback)
        {
            var timerId = Guid.NewGuid();
            var cts = new CancellationTokenSource();
            _timers[timerId] = cts;

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(duration), cts.Token);
                    if (!cts.Token.IsCancellationRequested)
                    {
                        _mainThreadContext.Post(_ => callback?.Invoke(), null);
                    }
                }
                catch (TaskCanceledException)
                {
                }
                finally
                {
                    _timers.TryRemove(timerId, out _);
                }
            });

            return timerId;
        }

        public void CancelAllTimers()
        {
            foreach (var timerId in _timers.Keys)
            {
                CancelTimer(timerId);
            }
        }

        private void CancelTimer(Guid timerId)
        {
            if (_timers.TryRemove(timerId, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }
        }
    }
}