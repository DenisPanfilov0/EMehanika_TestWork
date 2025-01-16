using System;
using UnityEngine;

namespace Code.Gameplay.Services.SpawnersServices.BombSpawnerService
{
    public interface IBombSpawnerService
    {
        void StartSpawn(Transform transform);
    }
}