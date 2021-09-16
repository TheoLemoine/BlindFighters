using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public class PositionNoise : MonoBehaviour
    {
        [SerializeField] private float force;
        
        private void FixedUpdate()
        {
            transform.localPosition += new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)) * Time.fixedDeltaTime;
        }
    }
}