using Code.Gameplay.Services.SpawnersServices.BombSpawnerService;
using Code.Gameplay.Services.SpawnersServices.HeartSpawnerService;
using Code.Gameplay.Services.SpawnersServices.SlimeSpawnerService;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Behaviour
{
    public class SpawnZone : MonoBehaviour
    {
        [Inject]
        public void Construct(IBombSpawnerService iBombSpawnerService, ISlimeSpawnerService slimeSpawnerService, IHeartSpawnerService heartSpawnerService)
        {
            iBombSpawnerService.StartSpawn(transform);
            slimeSpawnerService.StartSpawn(transform);
            heartSpawnerService.StartSpawn(transform);
        }
    }
}