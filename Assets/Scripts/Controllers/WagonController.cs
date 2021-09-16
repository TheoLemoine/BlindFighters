using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sound;
using UnityEngine;

namespace Controllers
{
    public class WagonController : MonoBehaviour
    {
        public Align WagonAlign { get; private set; }

        [SerializeField] private GameState state;
        [SerializeField] private List<APlayerController> _playerControllers;
    
        [SerializeField] private Transform _leftWheelTransform;
        [SerializeField] private Transform _rightWheelTransform;
    
        [SerializeField] private float _tiltAmount;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _maxAnimationTime;
        [SerializeField] private float _maxWindVolume;
        [SerializeField] private float _speedToMaxWindVolume;
        [SerializeField] private float _maxKartVolume;
        [SerializeField] private float _speedToMaxKartVolume;
        [SerializeField] private float _startPitch;
        [SerializeField] private float _maxPitch;
        [SerializeField] private float _speedToMaxPitch;

        [SerializeField] private KartSound kartSound;
        [SerializeField] private AudioSource kartRollMain;
        [SerializeField] private AudioSource kartRollMetallicLayer;
        [SerializeField] private AudioSource wind;
    
        private bool _isAnimating = false;
        private SoundManager _soundManager;
        private SoundManager.ContinuousSound _grindSound;

        private void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            kartRollMain.Play();
            kartRollMetallicLayer.Play();
            wind.Play();
        }

        private void Update()
        {
            bool allLeft = _playerControllers.All(c => c.GetCurrentAlign() == Align.Left);
            bool allRight = _playerControllers.All(c => c.GetCurrentAlign() == Align.Right);
            
            if (WagonAlign == Align.Center && allLeft)
            {
                StartCoroutine(AnimateTo(Align.Left));
            }
            else if(WagonAlign == Align.Center && allRight)
            {
                StartCoroutine(AnimateTo(Align.Right));
            }
            else if(WagonAlign != Align.Center && !allLeft && !allRight)
            {
                StartCoroutine(AnimateTo(Align.Center));
            }

            wind.volume = Mathf.Min(Mathf.Log(_maxWindVolume / _speedToMaxWindVolume * state.gameSpeed*10,10), _maxWindVolume);
            Debug.Log(wind.volume);
            //kartRollMain.pitch = Mathf.Min(_maxPitch / _speedToMaxPitch * state.gameSpeed, _maxPitch);
            kartRollMain.pitch = Mathf.Min(Mathf.Max((state.gameSpeed - 8) * ((state.gameSpeed - _startPitch) / (_speedToMaxPitch - 8)),_startPitch), _maxPitch);
            kartRollMetallicLayer.volume = Mathf.Min(_maxKartVolume / _speedToMaxKartVolume * state.gameSpeed, _maxKartVolume);
;
        }
        
        private IEnumerator AnimateTo(Align align)
        {
            if(_isAnimating) yield break;
            _isAnimating = true;
            
            Quaternion baseRotation = transform.parent.localRotation;
            Quaternion targetRotation;
            switch (align)
            {
                case Align.Left:
                    targetRotation = Quaternion.Euler(0, 0, _tiltAmount);
                    _grindSound = _soundManager.PlayContinuous(Align.Left, kartSound.GrindSound);
                    //kartRollMain.panStereo = -1;
                    break;
                case Align.Right:
                    targetRotation = Quaternion.Euler(0, 0, -_tiltAmount);
                    _grindSound = _soundManager.PlayContinuous(Align.Right, kartSound.GrindSound);
                    //kartRollMain.panStereo = 1;
                    break;
                case Align.Center:
                default:
                    targetRotation = Quaternion.identity;
                    if(_grindSound.ReplayRoutine != null)
                        _soundManager.StopContinuous(_grindSound);
                    //kartRollMain.panStereo = 0;
                    break;
            }

            if (baseRotation == Quaternion.identity)
            {
                if (align == Align.Left)
                {
                    transform.parent = _rightWheelTransform;
                } 
                else if(align == Align.Right)
                {
                    transform.parent = _leftWheelTransform;
                }
            }

            var computedAnimationTime = Mathf.Min(_maxAnimationTime, _animationTime / state.gameSpeed);
            for (float elapsed = 0f; elapsed <= computedAnimationTime; elapsed += Time.deltaTime)
            {
                var progress = elapsed / computedAnimationTime;
                progress = align == Align.Center ? progress * progress : Mathf.Sqrt(progress);
            
                transform.parent.localRotation = Quaternion.Slerp(baseRotation, targetRotation, progress);

                yield return null;
            }
        
            transform.parent.localRotation = targetRotation;

            WagonAlign = align;
            _isAnimating = false;
        }
    }
}