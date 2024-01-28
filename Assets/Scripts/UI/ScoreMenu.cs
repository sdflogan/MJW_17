
using DG.Tweening;
using MJW.Game;
using MJW.Scores;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MJW.UI
{
    public class ScoreMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _mainCg;
        [SerializeField] private CanvasGroup _kingCg;
        [SerializeField] private TMPro.TextMeshProUGUI _points;

        private void Awake()
        {
            gameObject.SetActive(false);
            _mainCg.alpha = 0;
            _kingCg.alpha = 0;
            _points.alpha = 0;

            GameEvents.OnGameEnd += OnGameEnd;
        }

        private void OnDestroy()
        {
            GameEvents.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd()
        {
            gameObject.SetActive(true);
            _points.text = ((int) ScoreManager.Instance.Points).ToString();

            var seq = DOTween.Sequence();

            seq.Append(_mainCg.DOFade(1, 2f).SetEase(Ease.Linear));
            seq.Append(_kingCg.DOFade(1, 5f).SetEase(Ease.Linear));
            seq.Append(_points.DOFade(1, 5f).SetEase(Ease.Linear));
            seq.Append(_kingCg.DOFade(1, 1f).SetEase(Ease.Linear));
            seq.AppendInterval(1f);
            seq.AppendCallback(() =>
            {
                SceneManager.LoadScene(0);
            });
            seq.Play();
        }
    }
}