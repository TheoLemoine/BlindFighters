using Controllers;
using UnityEngine;

namespace Obstacles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameState state;
        
        [SerializeField] private bool isLeftObstacle;
        [SerializeField] private bool isCenterObstacle;
        [SerializeField] private bool isRightObstacle;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerStay(Collider other)
        {
            var wagon = other.GetComponentInParent<WagonController>();

            if (isLeftObstacle && wagon.WagonAlign == Align.Left ||
                isCenterObstacle && wagon.WagonAlign == Align.Center ||
                isRightObstacle && wagon.WagonAlign == Align.Right)
            {
                state.TriggerObstacleCollision();
            }

            state.TriggerObstacleEvade();
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.back * state.gameSpeed;
        }
    }
}