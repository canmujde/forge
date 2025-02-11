using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.AudioSystem
{
    public static class AudioManager
    {
        private static Queue<AudioSource> _sourcePool = new Queue<AudioSource>();
        private static int sourceLimit = 30;
        private static Transform _poolParent;
        static AudioManager()
        {
            // _poolParent = new GameObject("_AudioSourcePool").transform;
            for (int i = 0; i < sourceLimit; i++)
            {
                var obj = new GameObject("AudioSource", typeof(AudioSource));
                obj.hideFlags = HideFlags.HideInHierarchy;
                // obj.transform.SetParent(_poolParent);
                _sourcePool.Enqueue(obj.GetComponent<AudioSource>());
            }
        }


        public static void PlayOneShotAtPosition(string clipName, Vector3 position, bool spatialize = false)
        {
            var source  = _sourcePool.Dequeue();

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
                    source.loop = false;
                    source.mute = false;
                    source.playOnAwake = false;
                    source.pitch = 1.0f;
                    source.volume = 1.0f;
                    source.spatialBlend = 1.0f;
                    source.clip = clip;
                    source.spatialize = spatialize;
                    source.gameObject.transform.position = position;
                    source.Play();
                    ReturnSourceToPoolAfterDelay(source, clip.length);
                }
            }
            
        }
        
        private static async void ReturnSourceToPoolAfterDelay(AudioSource source, float delay)
        {
            await Task.Delay((int)(delay * 1000));
        
            if (source != null)
            {
                source.Stop();
                source.clip = null;
                source.gameObject.SetActive(false);
                _sourcePool.Enqueue(source); 
            }
        }
        
    }
}