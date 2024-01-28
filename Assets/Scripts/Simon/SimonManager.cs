
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
        [SerializeField] private List<SheetDifficulty> _difficulty;
        [SerializeField] private List<SimonArea> _areas;
        [SerializeField] private Color _success;
        [SerializeField] private Color _error;

        [Header("Config")]
        [SerializeField] private float _simonMinStartSeconds;
        [SerializeField] private float _simonMaxStartSeconds;
        [SerializeField] private float _simonDurationSeconds;

        [SerializeField] private bool _debug;

        private float _currentSimonSeconds;
        private Coroutine _simonCoroutine;

        public Color SuccessColor => _success;
        public Color ErrorColor => _error;

        public bool EventRunning { get; private set; }

        public Difficulty CurrentDifficulty { get; private set; }

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

        public void OnSheetCompleted(int errors, InstrumentType instrument)
        {
            EventRunning = false;

            GameEvents.OnSimonEnd?.Invoke();

            OnGameReady();
        }

        public void StartSimon()
        {
            _simonCoroutine = StartCoroutine(SimonEvent());
        }

        private IEnumerator SimonEvent()
        {
            EventRunning = true;
            _currentSimonSeconds = _simonDurationSeconds;
            _areas[Random.Range(0, _areas.Count - 1)].LaunchArea();

            GameEvents.OnSimonStart?.Invoke();

            while (_currentSimonSeconds > 0 && EventRunning)
            {
                yield return new WaitForSeconds(0.5f);
                _currentSimonSeconds -= 0.5f;
            }

            if (EventRunning)
            {
                // TODO PENALIZAR
                GameEvents.OnSimonEnd?.Invoke();
                OnGameReady();
            }
        }

        public List<ButtonType> GenerateSheet()
        {
            var difData = _difficulty.Find(element => element.Diff == CurrentDifficulty);

            int sheetSize = Random.Range(difData.MinNotes, difData.MaxNotes);

            List<ButtonType> buttons = new List<ButtonType>();

            for (int i=0; i<sheetSize; i++)
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