
using MJW.Game;
using MJW.Instruments;
using MJW.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Simon
{
    public class SimonManager : Singleton<SimonManager>
    {
        [SerializeField] private List<SimonButtonData> _buttons;
        [SerializeField] private List<SheetData> _sheetData;
        [SerializeField] private List<SimonArea> _areas;
        [SerializeField] private Color _success;
        [SerializeField] private Color _error;

        public Sprite SuccessIcon;
        public Sprite FailIcon;

        [Header("Config")]
        [SerializeField] private float _simonMinStartSeconds;
        [SerializeField] private float _simonMaxStartSeconds;
        [SerializeField] private float _simonDurationSeconds;

        [SerializeField] private bool _debug;

        private List<int> _indexStory = new List<int>();

        private float _currentSimonSeconds;
        private Coroutine _simonCoroutine;

        public Color SuccessColor => _success;
        public Color ErrorColor => _error;

        public bool EventRunning { get; private set; }

        #region Unity events

        private void Awake()
        {
            GameEvents.OnGameReady += OnGameReady;
            GameEvents.OnSheetCompled += OnSheetCompleted;
            
        }

        private void OnDestroy()
        {
            GameEvents.OnGameReady -= OnGameReady;
            GameEvents.OnSheetCompled -= OnSheetCompleted;

            if (_simonCoroutine != null)
            {
                StopCoroutine(_simonCoroutine);
                _simonCoroutine = null;
            }
        }

        #endregion

        private void Start()
        {
            if (_debug) OnGameReady();
        }

        public void OnGameReady()
        {
            float waitTime = Random.Range(_simonMinStartSeconds, _simonMaxStartSeconds);

            Debug.Log("Next simon on: " + waitTime);

            Invoke(nameof(StartSimon), waitTime);
        }

        public void OnSheetCompleted(int errors, InstrumentType instrument, int remainingSeconds)
        {
            EventRunning = false;

            GameEvents.OnSimonEnd?.Invoke(errors);

            if (_simonCoroutine != null)
            {
                StopCoroutine(SimonEvent());
                _simonCoroutine = null;
            }

            Debug.LogError("Terminando simón por sheet");

            OnGameReady();
        }

        public void StartSimon()
        {
            if (_simonCoroutine != null)
            {
                StopCoroutine(_simonCoroutine);
                _simonCoroutine = null;
            }

            _simonCoroutine = StartCoroutine(SimonEvent());
        }

        private int RandomAreaIndex()
        {

        }

        private IEnumerator SimonEvent()
        {
            EventRunning = true;
            _currentSimonSeconds = _simonDurationSeconds;

            int randomIndex = Random.Range(0, _areas.Count - 1);

            Debug.LogError("Random index: " + randomIndex);

            _areas[randomIndex].LaunchArea();

            GameEvents.OnSimonStart?.Invoke();

            while (_currentSimonSeconds > 0 && EventRunning)
            {
                yield return new WaitForSeconds(0.5f);
                _currentSimonSeconds -= 0.5f;
            }

            if (EventRunning)
            {
                // TODO PENALIZAR
                Debug.LogError("Terminando simón por tiempo");
                GameEvents.OnSimonEnd?.Invoke(-1);
                OnGameReady();
            }

            _simonCoroutine = null;
        }

        public List<ButtonType> GenerateSheet()
        {
            var difData = _sheetData[Random.Range(0, _sheetData.Count)];

            List<ButtonType> buttons = new List<ButtonType>();

            for (int i=0; i<difData.NotesAmount; i++)
            {
                ButtonType randomButton = (ButtonType)Random.Range(0, 7);
                buttons.Add(randomButton);
            }

            return buttons;
        }

        public Sprite GetIcon(ButtonType button)
        {
            var data = _buttons.Find(element => element.Type == button);

            return data.Icon;
        }
    }
}