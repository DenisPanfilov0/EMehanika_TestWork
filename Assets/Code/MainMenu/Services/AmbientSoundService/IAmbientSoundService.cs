using System;

namespace Code.MainMenu.Services.AmbientSoundService
{
    public interface IAmbientSoundService
    {
        event Action AmbientSoundOff;
        event Action AmbientSoundOn;
        void SoundOff();
        void SoundOn();
        bool GetSoundState();
    }
}