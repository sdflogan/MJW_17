
using MJW.Game;
using MJW.Instruments;
using MJW.Utils;
using UnityEngine;

namespace MJW.Scores
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public float Points { get; private set; }

        [SerializeField] private float _noteSucessScore = 10f;
        [SerializeField] private float _noteFailedScore = -10f;
        [SerializeField] private float _timeMultipier = 0.5f;
        [SerializeField] private float _comboMultiplier = 1.2f;

        private int _comboCounter;

        #region Unity events

        private void Awake()
        {
            GameEvents.OnNoteSuccess += OnNoteSuccess;
            GameEvents.OnNoteFailed += OnNoteFailed;
            GameEvents.OnSheetCompled += OnSheetCompleted;

        }

        private void OnDestroy()
        {
            GameEvents.OnNoteSuccess -= OnNoteSuccess;
            GameEvents.OnNoteFailed -= OnNoteFailed;
            GameEvents.OnSheetCompled -= OnSheetCompleted;
        }

        #endregion

        #region Callbacks

        private void OnNoteSuccess(InstrumentType instrument)
        {
            _comboCounter++;

            Points += _noteSucessScore * _comboCounter;
        }

        private void OnNoteFailed(InstrumentType instrument)
        {
            _comboCounter = 0;

            Points += _noteFailedScore;
        }

        private void OnSheetCompleted(int errors, InstrumentType instrument, int remainingSeconds)
        {
            _comboCounter++;

            var timeScore = remainingSeconds * _timeMultipier;
            Points += _comboCounter * _comboMultiplier + timeScore - errors * _noteFailedScore;
        }

        #endregion
    }
}