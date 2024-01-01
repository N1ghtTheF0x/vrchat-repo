
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace NTF.VRChat
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    [AddComponentMenu("N1ghtTheF0x/VRChat/MultiAudioTrack")]
    public class MultiAudioTrack : UdonSharpBehaviour
    {
        public AudioSource[] AudioSources = { };
        public bool AutoPlay = false;
        public int Layer = 0;
        // public float TransitionSpeed = 0.1f; // TODO: impl
        // C# only
        public AudioSource Reference { get => AudioSources[0]; set => AudioSources[0] = value; }
        // internal
        private bool switching = false;
        private float target_volume = 1;
        private AudioSource TransitionSource;
        void Start()
        {
            target_volume = Reference.volume;
            for(int index = 1;index  < AudioSources.Length;index++)
            {
                AudioSource source = AudioSources[index];
                SyncAudioSources(source);
                source.mute = true;
            }
            if(AutoPlay)
                Play();
        }
        private void Update()
        {
            for(int index = 0;index < AudioSources.Length;index++)
            {
                AudioSource source = AudioSources[index];
                SyncAudioSources(source);
            }
        }
        private void SyncAudioSources(AudioSource source)
        {
            if (Reference.isPlaying)
                source.time = Reference.time;
            source.pitch = Reference.pitch;
            if (!switching)
                source.volume = Reference.volume;
            source.loop = Reference.loop;
            source.spatialize = Reference.spatialize;
            source.priority = Reference.priority;
            source.panStereo = Reference.panStereo;
            source.spatialBlend = Reference.spatialBlend;
            source.reverbZoneMix = Reference.reverbZoneMix;
            source.dopplerLevel = Reference.dopplerLevel;
            source.spread = Reference.spread;
            source.rolloffMode = Reference.rolloffMode;
            source.maxDistance = Reference.maxDistance;
            source.minDistance = Reference.minDistance;
        }
        /*
        private void TransitionLayer(bool mute)
        {
            if (!switching) return;
            TransitionSource.volume += mute ? -TransitionSpeed : TransitionSpeed;
            if (TransitionSource.volume < 0)
            {
                TransitionSource.volume = 0;
                switching = false;
                TransitionSource = null;
            }
            if (TransitionSource.volume >= target_volume)
            {
                TransitionSource.volume = target_volume;
                switching = false;
                TransitionSource = null;
            }
        }
        */
        public void Play()
        {
            for (int index = 0; index < AudioSources.Length; index++)
                AudioSources[index].Play();
        }
        public void Stop()
        {
            for (int index = 0; index < AudioSources.Length; index++)
                AudioSources[index].Stop();
        }
        public void PushLayer()
        {
            Layer++;
            if (Layer >= AudioSources.Length)
            {
                Layer = AudioSources.Length - 1;
                Debug.LogWarning("Reached Layer limit");
                return;
            }
            EnableLayer(Layer);
            Debug.Log(string.Format("Pushing Layer {0}",Layer));
        }
        public void PushToggle()
        {
            Layer++;
            if (Layer >= AudioSources.Length)
            {
                Layer = AudioSources.Length - 1;
                Debug.LogWarning("Reached Toggle Layer limit");
                return;
            }
            ToggleLayer(Layer);
            Debug.Log(string.Format("Pushing Toggle Layer {0}", Layer));
        }
        public void PopToggle()
        {
            Layer--;
            if (Layer < 0)
            {
                Layer = 0;
                Debug.LogWarning("Popping a Toggle Layer at 0");
                return;
            }
            ToggleLayer(Layer);
            Debug.Log(string.Format("Pop Toggle Layer {0}", Layer));
        }
        public void PopLayer()
        {
            Layer--;
            if(Layer < 0)
            {
                Layer = 0; 
                Debug.LogWarning("Popping a Layer at 0");
                return;
            }
            DisableLayer(Layer + 1);
            Debug.Log(string.Format("Pushing Layer {0}", Layer + 1));
        }
        public void EnableLayer(int layer)
        {
            MuteLayer(layer, false);
        }
        public void DisableLayer(int layer)
        {
            MuteLayer(layer, true);
        }
        public void MuteLayer(int layer,bool mute)
        {
            AudioSources[layer].mute = mute;
        }
        public void ToggleLayer(int layer)
        {
            for(int index = 0;index < AudioSources.Length;index++)
            {
                MuteLayer(index, index != layer);
            }
        }
    }
}
