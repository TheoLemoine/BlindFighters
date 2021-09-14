using System.Collections;
using System.Collections.Generic;
using Sound.Containers;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "container", menuName = "ScriptableObjects/Kart Sound Container", order = 0)]
    public class KartSound : ScriptableObject
    {
        public ASoundContainer IntroSound;
        public ASoundContainer GrindSound;
    }
}
