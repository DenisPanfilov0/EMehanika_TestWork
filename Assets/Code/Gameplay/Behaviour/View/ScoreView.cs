using Code.Gameplay.Services.GameScoreService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Behaviour.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        private IGameScoreService _gameScoreService;

        [Inject]
        public void Construct(IGameScoreService gameScoreService)
        {
            _gameScoreService = gameScoreService;
        }

        private void Start()
        {
            _gameScoreService.ScoreChange += ScoreUpdate;

            _score.text = "0";
        }

        private void OnDestroy()
        {
            _gameScoreService.ScoreChange -= ScoreUpdate;
        }

        private void ScoreUpdate(int value)
        {
            _score.text = value.ToString();
        }
    }
}