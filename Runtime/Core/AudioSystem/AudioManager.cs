using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.AudioSystem
{
    public static class AudioManager
    {
        private static Queue<AudioSource> _sourcePool = new Queue<AudioSource>();
        private const int SourceLimit = 75;

        static AudioManager()
        {
            for (int i = 0; i < SourceLimit; i++)
            {
                _sourcePool.Enqueue(new GameObject("AudioSource", typeof(AudioSource))
                {
                    hideFlags = HideFlags.HideInHierarchy
                }.GetComponent<AudioSource>());
            }
        }

        public static void Play2D(string clipName)
        {
            var source = _sourcePool.Dequeue();

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

            var source = _sourcePool.Dequeue();

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

            var source = _sourcePool.Dequeue();

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

        public static void Play2DRanged(string clipName, float start = 0.0f, float end = 1.0f)
        {
            start = Mathf.Clamp(start, 0f, 1f);
            end = Mathf.Clamp(end, 0f, 1f);

            if (start > end)
            {
                start = Mathf.Max(0f, end - 0.1f);
            }

            var source = _sourcePool.Dequeue();

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
            var source = _sourcePool.Dequeue();

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
            _sourcePool.Enqueue(source);
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