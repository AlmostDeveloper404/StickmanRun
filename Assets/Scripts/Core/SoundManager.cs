using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _allSounds;

        public const string SoundsKey = "Sound";
        public const string MusicKey = "Music";

        private void Awake()
        {
            _allSounds = new AudioSource[transform.childCount];

            for (int i = 0; i < _allSounds.Length; i++)
            {
                _allSounds[i] = transform.GetChild(i).GetComponent<AudioSource>();
            }
        }

        private void Start()
        {
            AudioListener.pause = PlayerPrefs.GetInt(SoundsKey) == 0 ? false : true;
        }

        public void PlaySound(AudioClip audioClip)
        {
            Debug.Log("yep");
            for (int i = 0; i < _allSounds.Length; i++)
            {
                AudioSource audioSource = _allSounds[i];

                if (audioSource.clip == audioClip)
                {
                    audioSource.Play();
                }

            }
        }


        public void SwitchSound()
        {
            AudioListener.pause = AudioListener.pause == true ? false : true;
            PlayerPrefs.SetInt(SoundsKey, AudioListener.pause == false ? 0 : 1);
        }

        public void PauseSounds()
        {
            AudioListener.volume = 0f;
        }

        public void ResumeSounds()
        {
            AudioListener.volume = 1f;
        }
    }
}

