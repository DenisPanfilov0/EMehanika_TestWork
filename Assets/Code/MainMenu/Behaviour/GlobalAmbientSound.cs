using Code.MainMenu.Services.AmbientSoundService;
using UnityEngine;
using Zenject;

namespace Code.MainMenu.Behaviour
{
    public class GlobalAmbientSound : MonoBehaviour
    {
        private static GlobalAmbientSound _instance;

        [SerializeField] private AudioClip ambientClip;
        [SerializeField] private float volume = 0.5f;
        private AudioSource _audioSource;

        [Inject]
        public void Construct(IAmbientSoundService ambientSoundService)
        {
            ambientSoundService.AmbientSoundOn += Play;
            ambientSoundService.AmbientSoundOff += Stop;
        }
    
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = ambientClip;
            _audioSource.loop = true;
            _audioSource.volume = volume;
            _audioSource.playOnAwake = false;
            _audioSource.Play(); 
        }


        public void SetVolume(float newVolume)
        {
            _audioSource.volume = Mathf.Clamp01(newVolume);
        }

        public void Play()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        public void Stop()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }
}