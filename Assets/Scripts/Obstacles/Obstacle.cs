using System;
using Controllers;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameState state;
        
        [SerializeField] private bool isLeftObstacle;
        [SerializeField] private bool isCenterObstacle;
        [SerializeField] private bool isRightObstacle;
        
        private void OnTriggerEnter(Collider other)
        {
            var wagon = other.GetComponentInParent<WagonController>();

            if (isLeftObstacle && wagon.WagonAlign == Align.Right ||
                isCenterObstacle && wagon.WagonAlign == Align.Center ||
                isRightObstacle && wagon.WagonAlign == Align.Left)
            {
                // loooooooose...
            }
        }

        private void Update()
        {
            
        }
    }
}