using System;

namespace Code.Gameplay.Services.HeartService
{
    public interface IHeartService
    {
        void IncreaseHeart();
        void DecreaseHeart();
        void Cleanup();
        event Action<int> HeartCountChange;
        int GetCountHeart();
    }
}