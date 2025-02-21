using UnityEngine;
using UnityEngine.Audio;

namespace DataModels
{
    [System.Serializable]
    public class AudioSourceProperties
    {
        public float volume;
        public float pitch;
        public float time;
        public int timeSamples;
        public AudioClip clip;
        public AudioMixerGroup outputAudioMixerGroup;
        public GamepadSpeakerOutputType gamepadSpeakerOutputType;
        public bool loop;
        public bool ignoreListenerVolume;
        public bool playOnAwake;
        public bool ignoreListenerPause;
        public AudioVelocityUpdateMode velocityUpdateMode;
        public float panStereo;
        public float spatialBlend;
        public bool spatialize;
        public bool spatializePostEffects;
        public float reverbZoneMix;
        public bool bypassEffects;
        public bool bypassListenerEffects;
        public bool bypassReverbZones;
        public float dopplerLevel;
        public float spread;
        public int priority;
        public bool mute;
        public float minDistance;
        public float maxDistance;
        public AudioRolloffMode rolloffMode;


        public AudioSourceProperties(float volume,
            float pitch, float time, int timeSamples, AudioClip clip, AudioMixerGroup outputAudioMixerGroup,
            GamepadSpeakerOutputType gamepadSpeakerOutputType, bool loop, bool ignoreListenerVolume, bool playOnAwake,
            bool ignoreListenerPause, AudioVelocityUpdateMode velocityUpdateMode, float panStereo, float spatialBlend,
            bool spatialize, bool spatializePostEffects, float reverbZoneMix, bool bypassEffects,
            bool bypassListenerEffects, bool bypassReverbZones, float dopplerLevel, float spread, int priority,
            bool mute, float minDistance, float maxDistance, AudioRolloffMode rolloffMode)
        {
            this.volume = volume;
            this.pitch = pitch;
            this.time = time;
            this.timeSamples = timeSamples;
            this.clip = clip;
            this.outputAudioMixerGroup = outputAudioMixerGroup;
            this.gamepadSpeakerOutputType = gamepadSpeakerOutputType;
            this.loop = loop;
            this.ignoreListenerVolume = ignoreListenerVolume;
            this.playOnAwake = playOnAwake;
            this.velocityUpdateMode = AudioVelocityUpdateMode.Auto;
            this.ignoreListenerPause = ignoreListenerPause;
            this.velocityUpdateMode = velocityUpdateMode;
            this.panStereo = panStereo;
            this.spatialBlend = spatialBlend;
            this.spatialize = spatialize;
            this.spatializePostEffects = spatializePostEffects;
            this.reverbZoneMix = reverbZoneMix;
            this.bypassEffects = bypassEffects;
            this.bypassListenerEffects = bypassListenerEffects;
            this.bypassReverbZones = bypassReverbZones;
            this.dopplerLevel = dopplerLevel;
            this.spread = spread;
            this.priority = priority;
            this.mute = mute;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            this.rolloffMode = rolloffMode;
        }
        
    }
}