using Code.Gameplay.Services.FallManagerService;
using Code.Gameplay.Services.HeartService;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Behaviour.View
{
    public class HeartItemView : MonoBehaviour
    {
        private IHeartService _heartService;
        private IFallManagerService _fallManagerService;

        [Inject]
        public void Construct(IHeartService heartService, IFallManagerService fallManagerService)
        {
            _fallManagerService = fallManagerService;
            _heartService = heartService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _heartService.IncreaseHeart();
                _fallManagerService.RemoveFallingObject(gameObject);
                Destroy(gameObject);
            }
        }
    }
}