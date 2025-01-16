using System;
using UnityEngine;

namespace Code.Configs.ItemSpawnerConfig
{
    [Serializable]
    public class ItemSpawnerConfig
    {
        public ItemSpawnerTypeId Id;
        public GameObject Prefab;
    }
}