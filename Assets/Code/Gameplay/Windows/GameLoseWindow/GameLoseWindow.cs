using Code.Gameplay.Services.GameStateService;
using Code.Infrastructure.WindowsService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Windows.GameLoseWindow
{
    public class GameLoseWindow : BaseWindow
    {
        [SerializeField] private Button _restartLevel;
        private IGameStateService _gameStateService;

        [Inject]
        public void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
            Id = WindowId.GameLoseWindow;
        }

        private void Start()
        {
            _restartLevel.onClick.AddListener(RestartLevel);
        }

        private void OnDestroy()
        {
            _restartLevel.onClick.RemoveListener(RestartLevel);
        }

        private void RestartLevel()
        {
            _gameStateService.RestartLevel();
        }
    }
}