using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameState state;
        [SerializeField] private Transform obstacleContainer;
        [SerializeField] private List<Obstacle> possibleObstacles;

        [SerializeField] private float distanceBetweenObstacles = 10f;

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
            var selectedObstacle = possibleObstacles[Random.Range(0, possibleObstacles.Count)];

            Instantiate(selectedObstacle, obstacleContainer);
        }

        public void Clear()
        {
            foreach (Transform obstacle in obstacleContainer) {
                Destroy(obstacle.gameObject);
            }
        }
    }
}