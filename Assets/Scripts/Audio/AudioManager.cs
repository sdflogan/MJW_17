using MJW.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Inspector properties

        [SerializeField] private AudioSource _audioSourceMusic;
        [SerializeField] private AudioSource _audioSourceSFX;
        [SerializeField] private AudioSource _audioSourceAMB;

        [SerializeField] private List<InspectorClip> _audioClips;

        #endregion

        #region Private properties

        private Dictionary<SoundType, AudioClip> _clips = new Dictionary<SoundType, AudioClip>();

        #endregion

        #region Private methods

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach(var clip in _audioClips)
            {
                if (_clips.ContainsKey(clip.SoundType))
                {
                    Debug.LogError("[Audio Manager] Duplicated Clip");
                }

                _clips[clip.SoundType] = clip.Clip;
            }
        }

        private AudioClip GetClip(SoundType soundType)
        {
            if (_clips.ContainsKey(soundType))
            {
                return _clips[soundType];
            }

            Debug.LogError("[Audio Manager] Clip not found.");

            return null;
        }

        #endregion

        public void PlayMusic(SoundType music)
        {
            _audioSourceMusic.clip = GetClip(music);
            _audioSourceMusic.Play();
        }

        public void PlayAmb(SoundType music)
        {
            _audioSourceAMB.clip = GetClip(music);
            _audioSourceAMB.Play();
        }

        public void SetMusicVolume(float volume)
        {
            _audioSourceMusic.volume = volume;
        }

        public void StopAmb()
        {
            _audioSourceAMB.Stop();
        }

        public void StopMusic()
        {
            _audioSourceMusic.Stop();
        }

        public void PlaySFX(SoundType sfx, float pitch = 1f)
        {
            _audioSourceSFX.pitch = pitch;
            _audioSourceSFX.PlayOneShot(GetClip(sfx));
        }

        public void StopSFX()
        {
            _audioSourceSFX.Stop();
        }

        public void StopAll()
        {
            StopMusic();
            StopSFX();
        }
    }
}