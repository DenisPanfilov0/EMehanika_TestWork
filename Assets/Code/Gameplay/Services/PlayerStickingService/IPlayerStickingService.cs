using System;
using Code.Gameplay.Behaviour.View;

namespace Code.Gameplay.Services.PlayerStickingService
{
    public interface IPlayerStickingService
    {
        void AddPlayer(PlayerView player);
        void Cleanup();
        void SlimeClicked(SlimeView slimeView);
        event Action<SlimeView> PlayerGlued;
        void FinishSticking();
        event Action OnFinishSticking;
    }
}