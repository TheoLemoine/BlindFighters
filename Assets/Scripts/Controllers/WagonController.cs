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

        [SerializeField] private KartSound kartSound;
    
        private bool _isAnimating = false;
        private SoundManager _soundManager;
        private SoundManager.ContinuousSound _grindSound;

        private void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
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
                    break;
                case Align.Right:
                    targetRotation = Quaternion.Euler(0, 0, -_tiltAmount);
                    _grindSound = _soundManager.PlayContinuous(Align.Right, kartSound.GrindSound);
                    break;
                case Align.Center:
                default:
                    targetRotation = Quaternion.identity;
                    if(_grindSound.ReplayRoutine != null)
                        _soundManager.StopContinuous(_grindSound);
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