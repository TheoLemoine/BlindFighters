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
        
        private float _timeSinceLastObstacle = 0f;
        
        private void Update()
        {
            var timeBetweenObstacles = distanceBetweenObstacles / state.gameSpeed;

            _timeSinceLastObstacle += Time.deltaTime;

            if (_timeSinceLastObstacle > timeBetweenObstacles)
            {
                SpawnObstacle();
                _timeSinceLastObstacle = 0;
            }
        }

        private void SpawnObstacle()
        {
            int obstacleIndex = Random.Range(0, possibleObstacles.Count);
            Instantiate(possibleObstacles[obstacleIndex], obstacleContainer);
        }

        public void Clear()
        {
            foreach (Transform obstacle in obstacleContainer) {
                Destroy(obstacle.gameObject);
            }
        }
    }
}