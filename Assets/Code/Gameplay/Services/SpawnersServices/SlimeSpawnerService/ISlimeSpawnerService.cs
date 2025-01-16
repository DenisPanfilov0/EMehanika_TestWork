using System;
using UnityEngine;

namespace Code.Gameplay.Services.SpawnersServices.SlimeSpawnerService
{
    public interface ISlimeSpawnerService
    {
        void StartSpawn(Transform spawnZoneTransform);
    }
}