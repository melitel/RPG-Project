using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectsPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistantPbjects();

            hasSpawned = true;
        }

        private void SpawnPersistantPbjects()
        {
            GameObject persistentObjects = Instantiate(persistentObjectsPrefab);
            DontDestroyOnLoad(persistentObjects);
        }
    }
}
