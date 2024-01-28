
using MJW.Audio;
using MJW.Utils;

namespace MJW.Game
{
    public class GameManager : Singleton<GameManager>
    {
        #region Unity events

        private void Awake()
        {
            GameEvents.OnGameReady += OnGameReady;
            GameEvents.OnGameEnd += OnGameEnd;

            AudioManager.Instance.PlayMusic(SoundType.rana_demo);
        }

        private void OnDestroy()
        {
            GameEvents.OnGameReady -= OnGameReady;
            GameEvents.OnGameEnd -= OnGameEnd;
        }

        #endregion

        #region Callbacks

        private void OnGameReady()
        {

        }

        private void OnGameEnd()
        {
            // TODO: Display Scoreboard
        }

        #endregion
    }
}