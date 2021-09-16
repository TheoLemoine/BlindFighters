using UnityEngine;

namespace Utils
{
    public class DestroyOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}