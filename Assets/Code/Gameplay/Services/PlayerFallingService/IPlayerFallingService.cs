using Code.Gameplay.Behaviour.View;

namespace Code.Gameplay.Services.PlayerFallingService
{
    public interface IPlayerFallingService
    {
        void AddPlayer(PlayerView player);
        void UpdatePlayerPosition();
        void PlayerFall();
        void StopFalling();
        void Cleanup();
    }
}