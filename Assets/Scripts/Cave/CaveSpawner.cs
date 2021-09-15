using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cave
{
    public class CaveSpawner : MonoBehaviour
    {
        [SerializeField] private GameState state;
        [SerializeField] private List<Cave> caves;

        [SerializeField] private float distanceBetweenCaves;
        [SerializeField] private int prefillAmount;

        IEnumerator Start()
        {
            Prefill();
            
            for (;;)
            {
                SpawnObstacle();
                yield return new WaitForSeconds(distanceBetweenCaves / state.gameSpeed);
            }
        }

        private void SpawnObstacle()
        {
            var selectedObstacle = caves[Random.Range(0, caves.Count)];

            Instantiate(selectedObstacle, transform);
        }

        private void Prefill()
        {
            for (int i = 0; i < prefillAmount; i++)
            {
                var selectedObstacle = caves[Random.Range(0, caves.Count)];
                var spawnedObstacle = Instantiate(selectedObstacle, transform);

                spawnedObstacle.transform.localPosition += Vector3.back * i * distanceBetweenCaves;
            }
        }
    }
}