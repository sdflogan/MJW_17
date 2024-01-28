
using DG.Tweening;
using MJW.Game;
using TMPro;
using UnityEngine;

namespace MJW.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _cg;
        [SerializeField] private TextMeshProUGUI _timeTxt;

        #region Unity events

        private void Awake()
        {
            GameEvents.OnGameReady += OnGameReady;
            GameEvents.OnGameEnd += OnGameEnd;
            GameEvents.OnTimeUpdated += OnTimeUpdated;

            _cg.alpha = 0;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameEvents.OnGameReady -= OnGameReady;
            GameEvents.OnGameEnd -= OnGameEnd;
            GameEvents.OnTimeUpdated -= OnTimeUpdated;
        }

        #endregion

        #region Callbacks

        private void OnTimeUpdated(int remainingSeconds)
        {
            _timeTxt.text = remainingSeconds.ToString();
        }

        private void OnGameReady()
        {
            gameObject.SetActive(true);
            _cg.DOFade(1, 1).SetEase(Ease.Linear).Play();
        }

        private void OnGameEnd()
        {
            _cg.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false)).Play();
        }

        #endregion

    }
}