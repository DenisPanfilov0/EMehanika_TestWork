using Code.Gameplay.Behaviour.View;
using Code.Gameplay.Services.GameStateService;
using UnityEngine;

namespace Code.Gameplay.Services.PlayerFallingService
{
    public class PlayerFallingService : IPlayerFallingService
    {
        private readonly IGameStateService _gameStateService;
        private readonly float _fallSpeed = 4.5f;
        private PlayerView _playerView;
        private bool _isCanFalling = true;
        private bool _isGameStop = false;
        private const float MinFallYPercentage = 0.45f;

        public PlayerFallingService(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;

            _gameStateService.OnGameLose += () =>
            {
                _isGameStop = true;
            };
        }
        
        public void AddPlayer(PlayerView player)
        {
            _playerView = player;
        }

        public void UpdatePlayerPosition()
        {
            if (_isCanFalling && !_isGameStop)
            {
                RectTransform rectTransform = _playerView.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    Vector3 currentPosition = rectTransform.localPosition;

                    float screenHeight = -Camera.main.pixelHeight;

                    float minFallY = screenHeight * MinFallYPercentage;

                    if (currentPosition.y > minFallY)
                    {
                        rectTransform.localPosition += Vector3.down * _fallSpeed;
                    }
                    else
                    {
                        rectTransform.localPosition = new Vector3(currentPosition.x, minFallY, currentPosition.z);
                    }
                }
            }
        }

        public void PlayerFall()
        {
            _isCanFalling = true;
        }

        public void StopFalling()
        {
            _isCanFalling = false;
        }

        public void Cleanup()
        {
            _isGameStop = false;
        }
    }
}