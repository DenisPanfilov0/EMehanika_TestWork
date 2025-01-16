using System.Collections.Generic;
using Code.Progress.Provider;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainMenu.Behaviour
{
    public class GameTutorial : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _slids;
        [SerializeField] private Button _nextSlde;
        
        private IProgressProvider _progress;
        private int _counter = 0;

        [Inject]
        public void Construct(IProgressProvider progress)
        {
            _progress = progress;
        }

        private void Start()
        {
            if (!_progress.ProgressData.IsTutorialChecked)
            {
                _nextSlde.onClick.AddListener(NextSlide);
                
                _slids[_counter].SetActive(true);
                _counter++;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void NextSlide()
        {
            if (_counter < _slids.Count)
            {
                _slids[_counter].SetActive(true);
                _counter++;
            }
            else
            {
                _progress.ProgressData.IsTutorialChecked = true;
                Destroy(gameObject);
            }
        }
    }
}