using System.Collections.Generic;
using Code.Gameplay.Services.HeartService;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Behaviour.View
{
    public class HeartContainerView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _hearts;
        
        private IHeartService _heartService;
        private int _heartCount;

        [Inject]
        public void Construct(IHeartService heartService)
        {
            _heartService = heartService;
        }

        private void Start()
        {
            _heartCount = _heartService.GetCountHeart();
            _heartService.HeartCountChange += HeartCountChange;
        }

        private void OnDestroy()
        {
            _heartService.HeartCountChange -= HeartCountChange;
        }

        private void HeartCountChange(int value)
        {
            if (value < _heartCount)
            {
                DecreaseHeart(value);
            }
            else
            {
                IncreaseHeart(value);
            }

            _heartCount = value;
        }

        private void DecreaseHeart(int value)
        {
            _hearts[value].transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                _hearts[value].SetActive(false);
            });
            
        }

        private void IncreaseHeart(int value)
        {
            _hearts[value - 1].transform.DOScale(Vector3.one, 0.25f).OnComplete(() =>
            {
                _hearts[value - 1].SetActive(true);
            });
        }
    }
}