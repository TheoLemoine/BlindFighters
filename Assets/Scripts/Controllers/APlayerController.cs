using System;
using System.Collections;
using Sound;
using UnityEngine;
using UnityEngine.Audio;

namespace Controllers
{
      public abstract class APlayerController : MonoBehaviour
      {
            [SerializeField] private float rotateAmount;
            [SerializeField] private float _animationTime;
            private bool _isAnimating;
            private Align _animationAlign = Align.Center;

            private SoundManager _soundManager;
            
            public abstract Align GetCurrentAlign();

            private void Update()
            {
                  if (!_isAnimating)
                  {
                        var nextAlign = GetCurrentAlign();
                        if (nextAlign != _animationAlign)
                        {
                              StartCoroutine(SmoothRotation(nextAlign));
                        }
                  }
            }

            private IEnumerator SmoothRotation(Align align)
            {
                  _isAnimating = true;
                  
                  Quaternion baseRotation = transform.localRotation;
                  Quaternion targetRotation;
                  switch (align)
                  {
                        case Align.Left:
                              targetRotation = Quaternion.Euler(0, 0, rotateAmount);
                              break;
                        case Align.Right:
                              targetRotation = Quaternion.Euler(0, 0, -rotateAmount);
                              break;
                        case Align.Center:
                        default:
                              targetRotation = Quaternion.identity;
                              break;
                  }
            
                  for (float elapsed = 0f; elapsed <= _animationTime; elapsed += Time.deltaTime)
                  {
                        var progress = elapsed / _animationTime;
                        progress = align == Align.Center ? progress * progress : Mathf.Sqrt(progress);
            
                        transform.localRotation = Quaternion.Slerp(baseRotation, targetRotation, progress);

                        yield return null;
                  }

                  transform.localRotation = targetRotation;
                  
                  _isAnimating = false;
                  _animationAlign = align;
            }
      }
}