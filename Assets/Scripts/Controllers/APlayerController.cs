using System.Collections;
using UnityEngine;

namespace Controllers
{
      public abstract class APlayerController : MonoBehaviour
      {
            [SerializeField] private float rotateAmount;
      
            [SerializeField] private float _animationTime;
            private bool _isAnimating;
      
            public abstract Align GetCurrentAlign();

            private void Update()
            {
                  if (!_isAnimating)
                  {
                        StartCoroutine(SmoothRotation(GetCurrentAlign()));
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

                  _isAnimating = false;
        
                  transform.localRotation = targetRotation;

            }
      }
}