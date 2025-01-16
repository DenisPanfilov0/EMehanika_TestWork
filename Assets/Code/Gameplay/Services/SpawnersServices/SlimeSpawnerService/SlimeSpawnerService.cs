using Code.Configs.ItemSpawnerConfig;
using Code.Gameplay.Services.FallManagerService;
using Code.Gameplay.Services.GameStateService;
using Code.Gameplay.Services.TimerService;
using Code.Infrastructure.StaticData;
using Code.Progress.Provider;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Services.SpawnersServices.SlimeSpawnerService
{
    public class SlimeSpawnerService : ISlimeSpawnerService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressProvider _progress;
        private readonly ITimerService _timerService;
        private readonly DiContainer _container;
        private readonly IFallManagerService _fallManagerService;
        private readonly IGameStateService _gameStateService;

        private GameObject _slimePrefab;
        private bool _isSpawningActive;

        public SlimeSpawnerService(IStaticDataService staticDataService, ITimerService timerService, IProgressProvider progress,
            DiContainer container, IFallManagerService fallManagerService, IGameStateService gameStateService)
        {
            _staticDataService = staticDataService;
            _timerService = timerService;
            _progress = progress;
            _container = container;
            _fallManagerService = fallManagerService;
            _gameStateService = gameStateService;

            _slimePrefab = GetSlimePrefab();
            
            _gameStateService.OnGameLose += StopSpawn;
        }

        public void StartSpawn(Transform spawnZoneTransform)
        {
            _isSpawningActive = true;
            StartSpawnLoop(spawnZoneTransform);
        }

        private void StopSpawn()
        {
            _isSpawningActive = false;
        }

        private void StartSpawnLoop(Transform spawnZoneTransform)
        {
            if (!_isSpawningActive) return;

            _timerService.StartTimer(Random.Range(1f, 1.7f), () =>
            {
                SpawnSlime(spawnZoneTransform);
                StartSpawnLoop(spawnZoneTransform);
            });

        }

        private void SpawnSlime(Transform spawnZoneTransform)
        {
            if (_isSpawningActive)
            {

                RectTransform rectTransform = spawnZoneTransform as RectTransform;
                if (rectTransform == null)
                {
                    return;
                }

                Vector2 slimeSize = _slimePrefab.GetComponent<RectTransform>().sizeDelta;

                float minX = rectTransform.position.x - rectTransform.rect.width / 2 + slimeSize.x / 2;
                float maxX = rectTransform.position.x + rectTransform.rect.width / 2 - slimeSize.x / 2;
                float minY = rectTransform.position.y - rectTransform.rect.height / 2 + slimeSize.y / 2;
                float maxY = rectTransform.position.y + rectTransform.rect.height / 2 - slimeSize.y / 2;

                Vector2 randomLocalPosition = new Vector2(
                    UnityEngine.Random.Range(minX, maxX),
                    UnityEngine.Random.Range(minY, maxY)
                );

                GameObject slime = _container.InstantiatePrefab(_slimePrefab, randomLocalPosition, Quaternion.identity,
                    spawnZoneTransform);

                _fallManagerService.AddFallingObject(slime);
            }
        }

        private GameObject GetSlimePrefab() => 
            _staticDataService.GetItemSpawnerPrefab(ItemSpawnerTypeId.Slime);
    }
}