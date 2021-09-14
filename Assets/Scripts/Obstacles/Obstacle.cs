using System;
using Controllers;
using Sound;
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
        [SerializeField] private ObstacleSound obstacleSound;
        [SerializeField] private Align soundAlignment;
        
        private Rigidbody _rb;
        private SoundManager _soundManager;
        private bool isPlay = false;
        

        private void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            _rb = GetComponent<Rigidbody>();
        }

        public void IsInObstacle(Collider other)
        {
            var wagon = other.GetComponentInParent<WagonController>();
            if (isLeftObstacle && wagon.WagonAlign == Align.Left ||
                isCenterObstacle && wagon.WagonAlign == Align.Center ||
                isRightObstacle && wagon.WagonAlign == Align.Right)
            {
                state.TriggerObstacleCollision();
                if (!isPlay)
                    _soundManager.PlaySound(soundAlignment, obstacleSound.HitSound.GetClip());
                isPlay = true;
            }
            state.TriggerObstacleEvade();
        }

        public void PassedPreObstacle(Collider other)
        {
            _soundManager.PlaySound(soundAlignment, obstacleSound.SignalSound.GetClip());
        }

        public void SignalPreObstacle(Collider other)
        {
            _soundManager.PlaySound(soundAlignment, obstacleSound.SignalSound.GetClip());
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.back * state.gameSpeed;
        }
    }
}