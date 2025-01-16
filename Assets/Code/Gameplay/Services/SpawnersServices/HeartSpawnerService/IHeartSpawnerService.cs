using System;
using UnityEngine;

namespace Code.Gameplay.Services.SpawnersServices.HeartSpawnerService
{
    public interface IHeartSpawnerService
    {
        void StartSpawn(Transform spawnZoneTransform);
    }
}