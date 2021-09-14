using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
        private bool _isAnimating = false;

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
            else if(!allLeft && !allRight)
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
                    break;
                case Align.Right:
                    targetRotation = Quaternion.Euler(0, 0, -_tiltAmount);
                    break;
                case Align.Center:
                default:
                    targetRotation = Quaternion.identity;
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