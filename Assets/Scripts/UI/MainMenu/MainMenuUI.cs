
using MJW.Game;
using UnityEngine;
using UnityEngine.UI;

namespace MJW.UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _exitBtn;

        #endregion

        #region Unity events

        private void Awake()
        {
            _playBtn.onClick.AddListener(OnPlayPressed);
            _exitBtn.onClick.AddListener(OnExitPressed);
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _exitBtn.onClick.RemoveAllListeners();
        }

        #endregion

        #region Private methods

        private void OnPlayPressed()
        {
            _playBtn.enabled = false;
            GameEvents.OnGameStarted?.Invoke();
        }

        private void OnExitPressed()
        {
            Application.Quit();
        }

        #endregion
    }
}