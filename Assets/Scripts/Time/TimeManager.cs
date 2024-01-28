
using MJW.Game;
using MJW.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace MJW.MyTime
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] private int _roundSeconds = 60;

        private Coroutine _coroutine;
        private float _currentSeconds;

        public float RemainingSeconds => _currentSeconds;

        #region Unity events

        private void Awake()
        {
            GameEvents.OnGameReady += OnGameReady;
        }

        private void OnDestroy()
        {
            GameEvents.OnGameReady -= OnGameReady;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        #endregion

        #region Callbacks

        private void OnGameReady()
        {
            _coroutine = StartCoroutine(TimeCoroutine());
        }

        #endregion

        #region Private

        private IEnumerator TimeCoroutine()
        {
            _currentSeconds = _roundSeconds;

            while (_currentSeconds > 0)
            {
                yield return new WaitForSeconds(1f);
                _currentSeconds -= 1f;

                GameEvents.OnTimeUpdated?.Invoke((int)_currentSeconds);
            }

            GameEvents.OnTimeUpdated?.Invoke(0);
            GameEvents.OnGameEnd?.Invoke();
        }

        #endregion
    }
}