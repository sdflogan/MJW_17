
using MJW.Game;
using MJW.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace MJW.MyTime
{
    public class TimeManager : Singleton<MonoBehaviour>
    {
        [SerializeField] private int _roundSeconds = 60;

        private Coroutine _coroutine;
        private float _currentSeconds;

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
                yield return new WaitForSeconds(0.5f);
                _currentSeconds -= 0.5f;

                GameEvents.OnTimeUpdated?.Invoke((int) Mathf.Round(_currentSeconds));
            }

            GameEvents.OnTimeUpdated?.Invoke(0);
            GameEvents.OnGameEnd?.Invoke();
        }

        #endregion
    }
}