using Code.Gameplay.Services.PlayerStickingService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Behaviour.View
{
    public class SlimeView : MonoBehaviour
    {
        [field:SerializeField] public Collider2D _slimeCollider2D { get; private set; }
        
        [SerializeField] private Button _slimeClick;

        private IPlayerStickingService _playerStickingService;

        [Inject]
        public void Construct(IPlayerStickingService playerStickingService)
        {
            _playerStickingService = playerStickingService;
        }

        private void Start()
        {
            _slimeClick.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _slimeClick.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _playerStickingService.SlimeClicked(this);
        }
    }
}