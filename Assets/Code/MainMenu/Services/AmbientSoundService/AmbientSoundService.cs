using System;

namespace Code.MainMenu.Services.AmbientSoundService
{
    public class AmbientSoundService : IAmbientSoundService
    {
        public event Action AmbientSoundOff;
        public event Action AmbientSoundOn;

        private bool _isSoundOff = false;

        public void SoundOff()
        {
            _isSoundOff = true;
            AmbientSoundOff?.Invoke();
        }

        public void SoundOn()
        {
            _isSoundOff = false;
            AmbientSoundOn?.Invoke();
        }

        public bool GetSoundState()
        {
            return _isSoundOff;
        }
    }
}