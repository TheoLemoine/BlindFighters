using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> { }
    
    public class EventOnCollide : MonoBehaviour
    {
        [SerializeField] private ColliderEvent triggerEnter;
        [SerializeField] private ColliderEvent triggerStay;
        [SerializeField] private ColliderEvent triggerExit;

        private void OnTriggerEnter(Collider other)
        {
            triggerEnter.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            triggerStay.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            triggerExit.Invoke(other);
        }
    }
}