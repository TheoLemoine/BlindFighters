using Controllers;
using Sound;
using UnityEngine;
using UnityEngine.Audio;

namespace Obstacles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameState state;
        [SerializeField] private AudioMixerGroup obstacleMixerGroup;
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

        #region Alignment
        public Align GetAlignment()
        {
            return alignment;
        }

        public string AlignmentToString()
        {
            Debug.Log(alignment);
            return (alignment == Align.Left) ? "Left" : ( (alignment == Align.Right) ? "Right" : "Center");
        }

        public int AlignmentToInt()
        {
           return (alignment == Align.Left) ? -1 : ( (alignment == Align.Right) ? 1 : 0);
        }
        #endregion

        public void IsInObstacle(Collider other)
        {
            var wagon = other.GetComponentInParent<WagonController>();
            /*
            if(isLeftObstacle && isRightObstacle)
            {
                if (!isPlay)
                    _soundManager.PlaySound(soundAlignment, obstacleSound.PreOngoingSound.GetClip());
            }
            */
            if (isLeftObstacle && wagon.WagonAlign == Align.Left ||
                isCenterObstacle && wagon.WagonAlign == Align.Center ||
                isRightObstacle && wagon.WagonAlign == Align.Right)
            {
                state.TriggerObstacleCollision();
                if (!isPlay)
                    _soundManager.PlaySound(alignment, obstacleSound.HitSound.GetClip(), obstacleMixerGroup);
                isPlay = true;
            }
            state.TriggerObstacleEvade();
        }

        public void PassedPreObstacle(Collider other)
        {
            _soundManager.PlaySound(alignment, obstacleSound.SignalSound.GetClip(), obstacleMixerGroup);
        }

        public void SignalPreObstacle(Collider other)
        {
            _soundManager.PlaySound(alignment, obstacleSound.SignalSound.GetClip(), obstacleMixerGroup);
        }

        public void OnEnterObstacleZone(Collider other)
        {
            state.TriggerOnEnterObstacleZone(gameObject.GetInstanceID());
        }

        public void OnExitObstacleZone(Collider other)
        {
            state.TriggerOnExitObstacleZone(gameObject.GetInstanceID());
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.back * state.gameSpeed;
            
            if(_rb.position.z < -70f) Destroy(gameObject);
        }
    }
}