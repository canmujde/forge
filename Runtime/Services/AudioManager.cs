using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using UnityEngine;

namespace Services
{
    public static class AudioManager
    {
        private static readonly Queue<AudioSource> SourcePool = new Queue<AudioSource>();
        private static readonly AudioSource BackgroundMusic;
        private const int SourceLimit = 75;
        private const float BackgroundTransitionDuration = 3f;

        static AudioManager()
        {
            BackgroundMusic = new GameObject("BackgroundMusic", typeof(AudioSource))
            {
                hideFlags = HideFlags.HideInHierarchy
            }.GetComponent<AudioSource>();
            
            Object.DontDestroyOnLoad(BackgroundMusic.gameObject);
            SetToDefault(BackgroundMusic, Default2D);
            BackgroundMusic.loop = true;
            for (int i = 0; i < SourceLimit; i++)
            {
                var source = new GameObject("AudioSource", typeof(AudioSource))
                {
                    hideFlags = HideFlags.HideInHierarchy
                }.GetComponent<AudioSource>();

                Object.DontDestroyOnLoad(source.gameObject);
                SourcePool.Enqueue(source);
            }
        }

        public static AudioSource ReserveAudioSource()
        {
            return SourcePool.Count > 0 ? SourcePool.Dequeue() : null;
        }

        public static void EnqueueAudioSource(AudioSource source)
        {
            SourcePool.Enqueue(source);
        }

        public static async void PlayBackgroundMusic(string clipName, bool transition)
        {
            var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
            if (clip == null)
            {
                Debug.LogWarning($"Background clip {clipName} not found");
                return;
            }

            if (!transition)
            {
                BackgroundMusic.volume = 1f;
                BackgroundMusic.clip = clip;
                BackgroundMusic.Play();
                return;
            }


            if (BackgroundMusic.isPlaying)
            {
                var temp = new GameObject("BackgroundMusicTemp", typeof(AudioSource))
                {
                    hideFlags = HideFlags.HideInHierarchy
                }.GetComponent<AudioSource>();
                SetToDefault(temp, Default2D);
                temp.loop = true;
                temp.clip = BackgroundMusic.clip;
                temp.time = BackgroundMusic.time;
                temp.volume = 0.5f;
                BackgroundMusic.volume = 0.5f;
                temp.Play();

                await Task.Delay(10);
                var startVolume = temp.volume;

                BackgroundMusic.volume = 0f;
                BackgroundMusic.clip = clip;
                BackgroundMusic.Play();

                for (float t = 0; t < BackgroundTransitionDuration; t += Time.deltaTime)
                {
                    temp.volume = Mathf.Lerp(startVolume, 0f, t / BackgroundTransitionDuration);
                    BackgroundMusic.volume = Mathf.Lerp(0f, 1f, t / BackgroundTransitionDuration);
                    await Task.Yield();
                }

                BackgroundMusic.volume = 1f;
            }
            else
            {
                BackgroundMusic.volume = 0f;
                BackgroundMusic.clip = clip;
                BackgroundMusic.Play();

                for (float t = 0; t < BackgroundTransitionDuration; t += Time.deltaTime)
                {
                    BackgroundMusic.volume = Mathf.Lerp(0f, 1f, t / BackgroundTransitionDuration);
                    await Task.Yield();
                }

                BackgroundMusic.volume = 1f;
            }
        }

        public static void Play2D(string clipName)
        {
            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default2D);
                    source.clip = clip;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
        }

        public static void Play2D(string clipName, float volume)
        {
            volume = Mathf.Clamp(volume, 0, 1);

            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default2D);
                    source.clip = clip;
                    source.volume = volume;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
        }

        public static void Play2DPitched(string clipName, float pitch)
        {
            pitch = Mathf.Clamp(pitch, -3, 3);

            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default2D);
                    source.clip = clip;
                    source.pitch = pitch;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
        }

        public static void Play2DTrimmed(string clipName, float start = 0.0f, float end = 1.0f)
        {
            start = Mathf.Clamp(start, 0f, 1f);
            end = Mathf.Clamp(end, 0f, 1f);

            if (start > end)
            {
                start = Mathf.Max(0f, end - 0.1f);
            }

            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default2D);
                    source.clip = clip;
                    source.time = start * clip.length;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, end * clip.length - start * clip.length);
                }
            }
        }


        public static void Play3D(string clipName, Vector3 position)
        {
            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default3D);
                    source.clip = clip;
                    source.gameObject.transform.position = position;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
        }

        public static void Play3D(string clipName, Vector3 position, float volume)
        {
            volume = Mathf.Clamp(volume, 0, 1);
            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default3D);
                    source.clip = clip;
                    source.volume = volume;
                    source.gameObject.transform.position = position;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
        }

        public static void Play3DPitched(string clipName, Vector3 position, float pitch)
        {
            pitch = Mathf.Clamp(pitch, -3, 3);

            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default3D);
                    source.clip = clip;
                    source.pitch = pitch;
                    source.gameObject.transform.position = position;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
        }

        public static void Play3DTrimmed(string clipName, Vector3 position, float start = 0.0f, float end = 1.0f)
        {
            start = Mathf.Clamp(start, 0f, 1f);
            end = Mathf.Clamp(end, 0f, 1f);

            if (start > end)
            {
                start = Mathf.Max(0f, end - 0.1f);
            }

            var source = SourcePool.Dequeue();

            if (source == null)
            {
                Debug.LogWarning("AudioSource pool is empty");
            }
            else
            {
                var clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip == null)
                {
                    Debug.LogWarning("Audio clip not found");
                }
                else
                {
                    SetToDefault(source, Default3D);
                    source.clip = clip;
                    source.time = start * clip.length;
                    source.gameObject.transform.position = position;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, end * clip.length - start * clip.length);
                }
            }
        }


        private static void SetToDefault(AudioSource target, AudioSourceProperties asDefault)
        {
            target.mute = asDefault.mute;
            target.bypassEffects = asDefault.bypassEffects;
            target.bypassListenerEffects = asDefault.bypassListenerEffects;
            target.bypassReverbZones = asDefault.bypassReverbZones;
            target.spatialize = asDefault.spatialize;
            target.spatializePostEffects = asDefault.spatializePostEffects;
            target.playOnAwake = asDefault.playOnAwake;
            target.loop = asDefault.loop;
            target.priority = asDefault.priority;
            target.volume = asDefault.volume;
            target.pitch = asDefault.pitch;
            target.panStereo = asDefault.panStereo;
            target.spatialBlend = asDefault.spatialBlend;
            target.reverbZoneMix = asDefault.reverbZoneMix;
            target.dopplerLevel = asDefault.dopplerLevel;
            target.spread = asDefault.spread;
            target.rolloffMode = asDefault.rolloffMode;
            target.minDistance = asDefault.minDistance;
            target.maxDistance = asDefault.maxDistance;
        }

        private static async void ReturnSourceToPoolAfterDelay(AudioSource source, float delay)
        {
            if (!source) return;

            await Task.Delay((int)(delay * 1000));

            source.Stop();
            source.clip = null;
            SourcePool.Enqueue(source);
        }


        private static readonly AudioSourceProperties Default3D =
            new(1.0f, 1.0f, 0,
                0, null, null, GamepadSpeakerOutputType.Speaker, false, default, false, default,
                AudioVelocityUpdateMode.Auto, 0, 1.0f, default, default, 1.0f, default, default, default, 1.0f, 0, 128,
                default, 1.0f, 500f, AudioRolloffMode.Logarithmic);


        private static readonly AudioSourceProperties Default2D =
            new(1.0f, 1.0f, 0,
                0, null, null, GamepadSpeakerOutputType.Speaker, false, default, false, default,
                AudioVelocityUpdateMode.Auto,
                0, 0.0f, default, default, 1.0f, default, default, default, 1.0f, 0, 128, default,
                1.0f, 500f, AudioRolloffMode.Logarithmic);
    }
}