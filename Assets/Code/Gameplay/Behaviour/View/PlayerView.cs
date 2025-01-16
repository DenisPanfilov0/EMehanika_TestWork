using System.Collections;
using Code.Gameplay.Services.PlayerFallingService;
using Code.Gameplay.Services.PlayerStickingService;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Behaviour.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _ropePrefab;

        private IPlayerFallingService _playerFallingService;
        private IPlayerStickingService _playerStickingService;
        private GameObject _ropeObject;
        private RectTransform _ropeTransform;
        private Coroutine _moveCoroutine;
        private float _moveSpeed = 1200f;

        [Inject]
        public void Construct(IPlayerStickingService playerStickingService, IPlayerFallingService playerFallingService)
        {
            _playerFallingService = playerFallingService;
            _playerStickingService = playerStickingService;
        }

        private void Start()
        {
            _playerStickingService.AddPlayer(this);
            _playerFallingService.AddPlayer(this);

            _playerStickingService.PlayerGlued += MoveToTarget;
            _playerStickingService.OnFinishSticking += HandleFinishSticking;
        }

        private void OnDestroy()
        {
            _playerStickingService.PlayerGlued -= MoveToTarget;
            _playerStickingService.OnFinishSticking -= HandleFinishSticking;
        }

        private void MoveToTarget(SlimeView slime)
        {
            if (slime == null)
            {
                return;
            }

            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            CreateRope();

            _moveCoroutine = StartCoroutine(MoveTowards(slime));
        }

        private void CreateRope()
        {
            if (_ropeObject != null)
            {
                Destroy(_ropeObject);
            }

            _ropeObject = Instantiate(_ropePrefab, _canvas.transform);
            _ropeTransform = _ropeObject.GetComponent<RectTransform>();

            _ropeTransform.SetSiblingIndex(1);

            _ropeTransform.position = transform.position;
            _ropeTransform.sizeDelta = new Vector2(27, 1);
        }


        private IEnumerator MoveTowards(SlimeView slime)
        {
            Collider2D slimeCollider = slime._slimeCollider2D;
            if (slimeCollider == null)
            {
                yield break;
            }

            while (!slimeCollider.bounds.Intersects(GetComponent<Collider2D>().bounds))
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    slime.transform.position,
                    _moveSpeed * Time.deltaTime
                );

                UpdateRope(slime);

                yield return null;
            }

            Destroy(_ropeObject);

            _playerStickingService.FinishSticking();
        }

        private void UpdateRope(SlimeView slime)
        {
            if (_ropeObject == null) return;

            Vector3 slimePosition = slime.transform.position;
            Vector3 playerPosition = transform.position;

            Vector2 canvasSlimePosition;
            Vector2 canvasPlayerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                Camera.main.WorldToScreenPoint(slimePosition),
                _canvas.worldCamera,
                out canvasSlimePosition
            );
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                Camera.main.WorldToScreenPoint(playerPosition),
                _canvas.worldCamera,
                out canvasPlayerPosition
            );

            float distance = Vector2.Distance(canvasPlayerPosition, canvasSlimePosition);

            _ropeTransform.anchoredPosition = (canvasPlayerPosition + canvasSlimePosition) / 2;
            _ropeTransform.sizeDelta = new Vector2(_ropeTransform.sizeDelta.x, distance);

            Vector2 direction = canvasSlimePosition - canvasPlayerPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _ropeTransform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        
        private void HandleFinishSticking()
        {
            if (_ropeObject != null)
            {
                Destroy(_ropeObject);
                _ropeObject = null;
            }

            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
        }
    }
}