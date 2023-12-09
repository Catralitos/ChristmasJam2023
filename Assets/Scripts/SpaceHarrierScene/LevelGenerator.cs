using System.Collections.Generic;
using SpaceHarrierScene.Lama;
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
        
        [SerializeField] private LayerMask obstaclesLayerMask;
        [SerializeField] private LayerMask floorLayerMask;
        
        [SerializeField] private float jetSpawnDistance = 100.0f;
        
        [SerializeField] private int stepSize;
        [SerializeField] private float floorDistance;

        [SerializeField] private float enemyRespawnTime = 10.0f;
        
        private int _currentStep;
        [SerializeField] private int startingObstacles;
        [SerializeField] private int startingEnemies;

        public GameObject tileObject;
        public List<GameObject> obstacles;
        public List<GameObject> enemies;

        private void Start()
        {
            _currentStep = 1;
            SpawnMoreLevel();
            InvokeRepeating(nameof(SpawnEnemies), enemyRespawnTime/2.0f, enemyRespawnTime);
        }

        public void SpawnMoreLevel()
        {
            if (LamaEntity.Instance == null) return;
            Instantiate(tileObject,
                new Vector3(tileObject.transform.position.x, -floorDistance, stepSize / 2.0f + stepSize * (_currentStep - 1)),
                Quaternion.identity);
            
            int c = 0;
            int spawnedObjects = 0;
            while (spawnedObjects < startingObstacles && c < 1000)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-stepSize/2, stepSize/2), 0,
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
            startingObstacles += startingObstacles / 3;
            startingEnemies += startingEnemies / 3;
        }

        private void SpawnEnemies()
        {
            if (LamaEntity.Instance == null) return;
            int c = 0;
            int spawnedEnemies = 0;
            while (spawnedEnemies < startingEnemies && c < 1000)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-stepSize/2, stepSize/2), 0,
                    LamaEntity.Instance.gameObject.transform.position.z + jetSpawnDistance);
                
                if (Physics.OverlapCapsule(spawnPos + Vector3.down * floorDistance,
                        spawnPos + Vector3.up * floorDistance, 5.0f, obstaclesLayerMask).Length < 1)
                {
                    GameObject toSpawn = enemies[Random.Range(0, enemies.Count)];
                    Vector3 localScale = toSpawn.transform.localScale;
                    spawnPos += new Vector3(0,
                        Random.Range(-floorDistance + localScale.y / 2.0f, floorDistance - localScale.y / 2.0f), 0);
                    GameObject spawned = Instantiate(toSpawn, spawnPos, Quaternion.identity);
                    spawned.transform.position = spawnPos;
                    spawnedEnemies++;
                }
                c++;
            }
        }
    }
}
