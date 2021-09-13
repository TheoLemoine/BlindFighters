using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class WagonController : MonoBehaviour
    {
        public Align WagonAlign { get; private set; }
    
        [SerializeField] private List<APlayerController> _playerControllers;
    
        [SerializeField] private Transform _leftWheelTransform;
        [SerializeField] private Transform _rightWheelTransform;
    
        [SerializeField] private float _tiltAmount;
        [SerializeField] private float _animationTime;
    
        private bool _isAnimating;

        private void Update()
        {
            if (_playerControllers.All(c => c.GetCurrentAlign() == Align.Left))
            {
                WagonAlign = Align.Left;
            }
            else if(_playerControllers.All(c => c.GetCurrentAlign() == Align.Right))
            {
                WagonAlign = Align.Right;
            }
            else
            {
                WagonAlign = Align.Center;
            }
        
            UpdateMesh();
        }

        private void UpdateMesh()
        {
            if (!_isAnimating)
            {
                StartCoroutine(AnimateTo(WagonAlign));
            }
        }

        private IEnumerator AnimateTo(Align align)
        {
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

            for (float elapsed = 0f; elapsed <= _animationTime; elapsed += Time.deltaTime)
            {
                var progress = elapsed / _animationTime;
                progress = align == Align.Center ? progress * progress : Mathf.Sqrt(progress);
            
                transform.parent.localRotation = Quaternion.Slerp(baseRotation, targetRotation, progress);

                yield return null;
            }
        
            transform.parent.localRotation = targetRotation;

            _isAnimating = false;
        }
    }
}