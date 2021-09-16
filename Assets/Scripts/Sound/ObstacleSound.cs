using Sound.Containers;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "sound", menuName = "ScriptableObjects/Obstacle Sounds", order = 0)]
    public class ObstacleSound : ScriptableObject
    {
        public ASoundContainer SignalSound;
        public ASoundContainer OngoingSound;
        public ASoundContainer HitSound;
    }
}