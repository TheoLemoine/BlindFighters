using System.Collections.Generic;
using UnityEngine;

namespace Sound.Containers
{
    [CreateAssetMenu(fileName = "container", menuName = "ScriptableObjects/Speed Sound Container", order = 0)]
    public class SpeedSoundContainer : ASoundContainer
    {
        public GameState state;
        public List<ASoundContainer> sounds;
        public List<float> speeds;

        public override AudioClip GetClip()
        {
            for (int i = 0; i < speeds.Count; i++)
            {
                if (speeds[i] > state.gameSpeed)
                {
                    return sounds[i - 1].GetClip();
                }
            }

            return sounds[speeds.Count - 1].GetClip();
        }
    }
}