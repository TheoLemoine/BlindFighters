using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameState state;
        [SerializeField] private Transform obstacleContainer;
        [SerializeField] private List<Obstacle> possibleObstacles;

        [SerializeField] private float distanceBetweenObstacles = 10f;

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
            var selectedObstacle = possibleObstacles[Random.Range(0, possibleObstacles.Count)];

            Instantiate(selectedObstacle, obstacleContainer);
        }
    }
}