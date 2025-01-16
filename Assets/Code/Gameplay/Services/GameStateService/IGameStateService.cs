using System;

namespace Code.Gameplay.Services.GameStateService
{
    public interface IGameStateService
    {
        event Action OnGameLose;
        void GameLose();
        void RestartLevel();
    }
}