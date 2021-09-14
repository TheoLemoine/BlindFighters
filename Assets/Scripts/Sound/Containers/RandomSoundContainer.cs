using System.Collections.Generic;
using UnityEngine;

namespace Sound.Containers
{
    [CreateAssetMenu(fileName = "container", menuName = "ScriptableObjects/Random Sound Container", order = 0)]
    public class RandomSoundContainer : ASoundContainer
    {
        public List<AudioClip> clips;

        public override AudioClip GetClip() => clips[Random.Range(0, clips.Count)];
    }
}