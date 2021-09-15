using Controllers.Arduino;
using UnityEngine;

namespace Controllers
{
    public class ArduinoReceiveController : APlayerController
    {
        [SerializeField] private float alignThreshold;
        
        private float _analogAlignValue;
        
        public override Align GetCurrentAlign()
        {
            if (_analogAlignValue > alignThreshold)
            {
                return Align.Right;
            }
            
            if (_analogAlignValue < -alignThreshold)
            {
                return Align.Left;
            }

            return Align.Center;
        }

        public void ReceiveData(float data)
        {
            _analogAlignValue = data;
        }
    }
}