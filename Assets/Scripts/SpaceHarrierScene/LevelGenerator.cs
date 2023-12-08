using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceHarrierScene
{
    public class LevelGenerator : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static LevelGenerator Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none already exists).
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
        
        public LayerMask obstaclesLayerMask;
        public LayerMask floorLayerMask;
        
        public int stepSize;
        public float floorDistance;

        public float enemyRespawnTime;
        
        private int _currentStep;
        private int _obstaclesToSpawn = 20;
        
        public GameObject tileObject;
        public List<GameObject> obstacles;
        public List<GameObject> enemies;

        private void Start()
        {
            _currentStep = 1;
            SpawnMoreLevel();
            InvokeRepeating(nameof(SpawnEnemies), enemyRespawnTime, enemyRespawnTime);
        }

        public void SpawnMoreLevel()
        {
            Instantiate(tileObject,
                new Vector3(tileObject.transform.position.x, -floorDistance, stepSize / 2.0f + stepSize * (_currentStep - 1)),
                Quaternion.identity);
            
            int c = 0;
            int spawnedObjects = 0;
            while (spawnedObjects < _obstaclesToSpawn && c < 1000)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-stepSize, stepSize), 0,
                    Random.Range(stepSize * (_currentStep - 1), stepSize * _currentStep));
                
                if (Physics.OverlapCapsule(spawnPos + Vector3.down * floorDistance,
                        spawnPos + Vector3.up * floorDistance, 5.0f, obstaclesLayerMask).Length < 1)
                {
                    if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, floorDistance, floorLayerMask))
                    {
                        GameObject toSpawn = obstacles[Random.Range(0, obstacles.Count)];
                        Instantiate(toSpawn, hit.point + new Vector3(0,toSpawn.transform.localScale.y/2.0f, 0), Quaternion.identity);
                        spawnedObjects++;
                    }

                }
                c++;
            }
            
            _currentStep++;
        }

        private void SpawnEnemies()
        {
            
        }
    }
}
