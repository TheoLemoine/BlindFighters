using System;
using UnityEngine;

namespace Cave
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cave : MonoBehaviour
    {
        [SerializeField] private GameState state;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.back * state.gameSpeed;
            
            if(_rb.position.z < -70f) Destroy(gameObject);
        }
    }
}