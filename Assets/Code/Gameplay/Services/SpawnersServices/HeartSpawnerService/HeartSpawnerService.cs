using Code.Configs.ItemSpawnerConfig;
using Code.Gameplay.Services.FallManagerService;
using Code.Gameplay.Services.GameStateService;
using Code.Gameplay.Services.TimerService;
using Code.Infrastructure.StaticData;
using Code.Progress.Provider;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Services.SpawnersServices.HeartSpawnerService
{
    public class HeartSpawnerService : IHeartSpawnerService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressProvider _progress;
        private readonly ITimerService _timerService;
        private readonly DiContainer _container;
        private readonly IFallManagerService _fallManagerService;
        private readonly IGameStateService _gameStateService;

        private GameObject _bombPrefab;
        private bool _isSpawningActive;

        public HeartSpawnerService(IStaticDataService staticDataService, ITimerService timerService, IProgressProvider progress, 
            DiContainer container, IFallManagerService fallManagerService, IGameStateService gameStateService)
        {
            _staticDataService = staticDataService;
            _timerService = timerService;
            _progress = progress;
            _container = container;
            _fallManagerService = fallManagerService;
            _gameStateService = gameStateService;

            _bombPrefab = GetBombPrefab();
            
            _gameStateService.OnGameLose += StopSpawn;
        }

        public void StartSpawn(Transform spawnZoneTransform)
        {
            _isSpawningActive = true;
            StartSpawnLoop(spawnZoneTransform);
        }

        private void StartSpawnLoop(Transform spawnZoneTransform)
        {
            if (!_isSpawningActive) return;

            _timerService.StartTimer(Random.Range(2.1f, 3.5f), () =>
            {
                SpawnBomb(spawnZoneTransform);
                StartSpawnLoop(spawnZoneTransform);
            });

        }

        private void StopSpawn()
        {
            _isSpawningActive = false;
        }

        private void SpawnBomb(Transform spawnZoneTransform)
        {
            if (_isSpawningActive)
            {

                RectTransform rectTransform = spawnZoneTransform as RectTransform;
                if (rectTransform == null)
                {
                    return;
                }

                Vector2 bombSize = _bombPrefab.GetComponent<RectTransform>().sizeDelta;

                float minX = rectTransform.position.x - rectTransform.rect.width / 2 + bombSize.x / 2;
                float maxX = rectTransform.position.x + rectTransform.rect.width / 2 - bombSize.x / 2;
                float minY = rectTransform.position.y - rectTransform.rect.height / 2 + bombSize.y / 2;
                float maxY = rectTransform.position.y + rectTransform.rect.height / 2 - bombSize.y / 2;

                Vector2 randomLocalPosition = new Vector2(
                    UnityEngine.Random.Range(minX, maxX),
                    UnityEngine.Random.Range(minY, maxY)
                );

                GameObject bomb = _container.InstantiatePrefab(_bombPrefab, randomLocalPosition, Quaternion.identity,
                    spawnZoneTransform);

                _fallManagerService.AddFallingObject(bomb);
            }
        }

        private GameObject GetBombPrefab() => 
            _staticDataService.GetItemSpawnerPrefab(ItemSpawnerTypeId.Heart);
    }
}