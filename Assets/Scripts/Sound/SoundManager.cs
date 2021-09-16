using System.Collections;
using Sound.Containers;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private GameState state;
        [SerializeField] private AudioSource master;
        [SerializeField] private AudioSource[] slaves;

        [SerializeField] private float _maxVolume;

        [SerializeField] private AudioSource Brass;
        [SerializeField] private AudioSource Drums;
        [SerializeField] private AudioSource Strings_Soft;
        [SerializeField] private AudioSource Strings_Hard;
        [SerializeField] private AudioSource Drums_Hard;
        [SerializeField] private AudioSource MUS_Timer;
        [SerializeField] private AudioSource MUS_Timer_02;

        [SerializeField] private AudioSource VO_Timer_4_min;
        [SerializeField] private AudioSource VO_Timer_3_min;
        [SerializeField] private AudioSource VO_Timer_2_min;
        [SerializeField] private AudioSource VO_Timer_1_min;
        [SerializeField] private AudioSource VO_Timer_30_s;
        [SerializeField] private AudioSource VO_Timer_10_s;

        [SerializeField] private AudioSource AMB_Cave;


        public void Start()
        {
            StartCoroutine(SyncSources());
        }

        public void Update()
        {
            Brass.volume = (state.gameSpeed - 10) * 0.5f;
            Strings_Hard.volume = (state.gameSpeed - 14) * 0.5f;
            Drums_Hard.volume = (state.gameSpeed - 15) * 0.5f;
            if(state.gameSpeed < 15)
                Drums.volume = (state.gameSpeed - 10) * 0.5f;
            else
                Drums.volume = (state.gameSpeed - 15) * -0.5f;
            if(state.gameSpeed < 14)
                Strings_Soft.volume = (state.gameSpeed - 8) * 0.5f;
            else
                Strings_Soft.volume = (state.gameSpeed - 14) * -0.5f;
            if (state.gameTimer > 30)
                MUS_Timer.volume = 0;
            else
                MUS_Timer.volume = 1;
            if (state.gameTimer > 10)
                MUS_Timer_02.volume = 0;
            else
                MUS_Timer_02.volume = 1;

            if (state.gameTimer < 240 && state.gameTimer > 239 && !VO_Timer_4_min.isPlaying)
            {
                VO_Timer_4_min.Play();
            }
            if (state.gameTimer < 180 && state.gameTimer > 179 && !VO_Timer_3_min.isPlaying)
            {
                VO_Timer_3_min.Play();
            }
            if (state.gameTimer < 120 && state.gameTimer > 119 && !VO_Timer_2_min.isPlaying)
            {
                VO_Timer_2_min.Play();
            }
            if (state.gameTimer < 60 && state.gameTimer > 59 && !VO_Timer_1_min.isPlaying)
            {
                VO_Timer_1_min.Play();
            }
            if (state.gameTimer < 30 && state.gameTimer > 29 && !VO_Timer_30_s.isPlaying)
            {
                VO_Timer_30_s.Play();
            }
            if (state.gameTimer < 10 && state.gameTimer > 9 && !VO_Timer_10_s.isPlaying)
            {
                VO_Timer_10_s.Play();
            }
        }

        private IEnumerator SyncSources()
        {
            while (true)
            {
                foreach (var slave in slaves)
                {
                    slave.timeSamples = master.timeSamples;
                    yield return null;
                }
            }
        }

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