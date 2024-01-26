
using DG.Tweening;
using MJW.Game;
using MJW.Utils;
using UnityEngine;

namespace MJW.Transition
{
    public class GameStartTransition : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private float _secondsDuration = 2f;
        [SerializeField] private Point3D _cameraGameplayPosition;

        [SerializeField] private CanvasGroup _mainMenuGroup;
        [SerializeField] private GameObject _mainMenu;

        [SerializeField] private Camera _camera;

        #endregion

        #region Private properties

        private Vector3 _menuPosition;
        private Quaternion _menuRotation;

        #endregion

        #region Unity events

        private void Awake()
        {
            _menuPosition = _camera.gameObject.transform.position;
            _menuRotation = _camera.gameObject.transform.rotation;

            GameEvents.OnGameStarted += OnGameStarted;
        }

        private void OnDestroy()
        {
            GameEvents.OnGameStarted -= OnGameStarted;
        }

        #endregion

        #region Private methods

        private void OnGameStarted()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_camera.gameObject.transform.
                DOMove(_cameraGameplayPosition.Position, _secondsDuration).SetEase(Ease.InOutQuad));
            
            sequence.Join(_camera.gameObject.transform.
                DORotate(_cameraGameplayPosition.Rotation.eulerAngles, _secondsDuration).SetEase(Ease.InOutQuad));

            sequence.Join(_mainMenuGroup.DOFade(0, _secondsDuration * .5f));
            sequence.AppendCallback(() => GameEvents.OnGameReady?.Invoke());

            sequence.Play();
        }

        #endregion
    }
}