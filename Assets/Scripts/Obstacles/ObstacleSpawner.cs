using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public static ObstacleSpawner instance = null;
        public List<Obstacle> possibleObstacles;
        
        [SerializeField] private GameState state;
        [SerializeField] private Transform obstacleContainer;
        [SerializeField] private float distanceBetweenObstacles = 10f;

        private void Awake() {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return; //Avoid doing anything else
            }
            instance = this;
        }
        
        IEnumerator Start()
        {
            for (;;)
            {
                SpawnObstacle();
                yield return new WaitForSeconds(distanceBetweenObstacles / state.gameSpeed);
            }
        }

        private void SpawnObstacle()
        {
            int obstacleIndex = Random.Range(0, possibleObstacles.Count);
            GameManager.instance.ObstacleSpawn.Invoke(obstacleIndex);
            Instantiate(possibleObstacles[obstacleIndex], obstacleContainer);
        }
    }
}