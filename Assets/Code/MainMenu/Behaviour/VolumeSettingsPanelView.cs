using Code.MainMenu.Services.AmbientSoundService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainMenu.Behaviour
{
    public class VolumeSettingsPanelView : MonoBehaviour
    {
        [SerializeField] private Image _soundImage;
        [SerializeField] private Button _changeSoundState;
        
        private IAmbientSoundService _ambientSoundService;

        [Inject]
        public void Construct(IAmbientSoundService ambientSoundService)
        {
            _ambientSoundService = ambientSoundService;
        }

        private void Start()
        {
            ChangeColorImage();

            _changeSoundState.onClick.AddListener(ChangeSoundState);
        }

        private void ChangeSoundState()
        {
            if (_ambientSoundService.GetSoundState())
            {
                _ambientSoundService.SoundOn();
            }
            else
            {
                _ambientSoundService.SoundOff();
            }

            ChangeColorImage();
        }

        private void ChangeColorImage()
        {
            if (_ambientSoundService.GetSoundState())
            {
                _soundImage.color = Color.gray;
            }
            else
            {
                _soundImage.color = Color.white;
            }
        }
    }
}