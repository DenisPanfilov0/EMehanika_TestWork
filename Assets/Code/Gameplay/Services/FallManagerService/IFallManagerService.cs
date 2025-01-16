using UnityEngine;

namespace Code.Gameplay.Services.FallManagerService
{
    public interface IFallManagerService
    {
        void AddFallingObject(GameObject fallingObject);
        void UpdateFallingObjects();
        void Cleanup();
        void RemoveFallingObject(GameObject fallingObject);
    }
}