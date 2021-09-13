using UnityEngine;

namespace Controllers
{
    public class KeyboardPlayerController : APlayerController
    {
        [SerializeField] private string leftKey;
        [SerializeField] private string rightKey;
    
        public override Align GetCurrentAlign()
        {
            if (Input.GetKey(leftKey))
                return Align.Left;
        
            if (Input.GetKey(rightKey))
                return Align.Right;
        
            return Align.Center;
        }
    }
}