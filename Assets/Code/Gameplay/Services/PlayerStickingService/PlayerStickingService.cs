using System;
using Code.Gameplay.Behaviour.View;
using Code.Gameplay.Services.GameStateService;
using Code.Gameplay.Services.PlayerFallingService;
using UnityEngine;

namespace Code.Gameplay.Services.PlayerStickingService
{
    public class PlayerStickingService : IPlayerStickingService
    {
        private readonly IPlayerFallingService _playerFallingService;
        public event Action<SlimeView> PlayerGlued;
        public event Action OnFinishSticking;
        
        private PlayerView _playerView;
        private SlimeView _currentStickSlime;
        private bool _isGlued = false;
        private readonly IGameStateService _gameStateService;

        public PlayerStickingService(IPlayerFallingService playerFallingService, IGameStateService gameStateService)
        {
            _playerFallingService = playerFallingService;
            _gameStateService = gameStateService;

            _gameStateService.OnGameLose += FinishSticking;
        }
        
        public void AddPlayer(PlayerView player)
        {
            _playerView = player;
        }

        public void Cleanup()
        {
            _playerView = null;
            _isGlued = false;
        }

        public void SlimeClicked(SlimeView slimeView)
        {
            if (!_isGlued && CanStickToSlime(slimeView))
            {
                _currentStickSlime = slimeView;
                StickPlayerToSlime(slimeView);
            }
            else if (_isGlued && CanStickToSlime(slimeView) && slimeView != _currentStickSlime)
            {
                _currentStickSlime = slimeView;
                StickPlayerToSlime(slimeView);
            }
            else if (slimeView == _currentStickSlime)
            {
                FinishSticking();
            }
        }

        public void FinishSticking()
        {
            _isGlued = false;
            _playerFallingService.PlayerFall();
            OnFinishSticking?.Invoke();
        }
        
        private bool CanStickToSlime(SlimeView slimeView)
        {
            Vector3 playerPosition = _playerView.transform.position;
            Vector3 slimePosition = slimeView.transform.position;

            float screenHeight = Camera.main.pixelHeight;

            float minDistanceY = screenHeight * 0.10f;

            if (slimePosition.y > playerPosition.y && (slimePosition.y - playerPosition.y) >= minDistanceY)
            {
                return true;
            }

            return false;
        }


        private void StickPlayerToSlime(SlimeView slimeView)
        {
            _isGlued = true;
            _playerFallingService.StopFalling();
            PlayerGlued?.Invoke(slimeView);
        }
    }
}