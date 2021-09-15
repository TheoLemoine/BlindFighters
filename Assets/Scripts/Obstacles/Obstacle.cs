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
        [SerializeField] private Align alignment;
        
        private Rigidbody _rb;
        private SoundManager _soundManager;
        private bool isPlay = false;
        
        private void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            _rb = GetComponent<Rigidbody>();
        }

        public string AlignmentToString()
        {
            Debug.Log(alignment);
            return (alignment == Align.Left) ? "Left" : ( (alignment == Align.Right) ? "Right" : "Center");
        }

        public int GetAlignment()
        {
           return (alignment == Align.Left) ? -1 : ( (alignment == Align.Right) ? 1 : 0);
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
                    _soundManager.PlaySound(alignment, obstacleSound.HitSound.GetClip());
                isPlay = true;
            }
            state.TriggerObstacleEvade();
        }

        public void PassedPreObstacle(Collider other)
        {
            _soundManager.PlaySound(alignment, obstacleSound.SignalSound.GetClip());
        }

        public void SignalPreObstacle(Collider other)
        {
            _soundManager.PlaySound(alignment, obstacleSound.SignalSound.GetClip());
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.back * state.gameSpeed;
        }
    }
}