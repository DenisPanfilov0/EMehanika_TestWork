using System;

namespace Code.Gameplay.Services.GameScoreService
{
    public interface IGameScoreService
    {
        void ScoreUpdate();
        void Cleanup();
        event Action<int> ScoreChange;
    }
}