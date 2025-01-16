using System;
using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.WindowsService;

namespace Code.Gameplay.Services.GameStateService
{
    public class GameStateService : IGameStateService
    {
        public event Action OnGameLose;
        
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _gameStateMachine;

        public GameStateService(IWindowService windowService, IGameStateMachine gameStateMachine)
        {
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
        }

        public void GameLose()
        {
            OnGameLose?.Invoke();
            _windowService.Open(WindowId.GameLoseWindow);
        }

        public void RestartLevel()
        {
            _gameStateMachine.Enter<RestartLevelState>();
        }
    }
}