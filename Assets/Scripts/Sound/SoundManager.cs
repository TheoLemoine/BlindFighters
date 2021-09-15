using System.Collections;
using Sound.Containers;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {

        public void PlaySound(Align align, AudioClip audioClip)
        {
            StartCoroutine(CoPlaySound(align, audioClip));
        }

        public IEnumerator CoPlaySound(Align align, AudioClip audioClip)
        {
            var source = SetupSource(align, audioClip);
            
            yield return new WaitForSeconds(audioClip.length);
            
            Destroy(source);
        }
        
        public struct ContinuousSound
        {
            public AudioSource Source;
            public Coroutine ReplayRoutine;
        }
        
        public ContinuousSound PlayContinuous(Align align, ASoundContainer container)
        {
            var source = SetupSource(align, null);
            var routine = StartCoroutine(ReplayContinuous(source, container));
            
            return new ContinuousSound
            {
                Source = source,
                ReplayRoutine = routine,
            };
        }

        public IEnumerator ReplayContinuous(AudioSource source, ASoundContainer container)
        {
            for (;;)
            {
                var nextClip = container.GetClip();
                source.clip = nextClip;
                source.Play();
                yield return new WaitForSeconds(nextClip.length);
            }
        }

        public void StopContinuous(ContinuousSound sound)
        {
            StopCoroutine(sound.ReplayRoutine);
            sound.Source.Stop();
            Destroy(sound.Source);
        }
        
        public AudioSource SetupSource(Align align, AudioClip audioClip)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = audioClip;
            source.panStereo = PanFromAlign(align);
            source.Play();

            return source;
        }

        public static float PanFromAlign(Align align)
        {
            switch (align)
            {
                case Align.Left: 
                    return -1;
                case Align.Right: 
                    return 1;
                case Align.Center:
                default :
                    return 0;
            }
        }
    }
}