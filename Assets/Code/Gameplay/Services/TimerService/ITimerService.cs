using System;

namespace Code.Gameplay.Services.TimerService
{
    public interface ITimerService
    {
        Guid StartTimer(float duration, Action callback);
        void CancelAllTimers();
    }
}