
using DG.Tweening;
using MJW.Game;
using MJW.Simon;
using MJW.Simon.UI;
using MJW.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Simon
{
    public class SimonArea : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private SimonButtonUI _buttonPrefab;
        [SerializeField] private CanvasGroup _hud;

        [Header("Config")]
        [SerializeField] private float _secondsFade;

        private List<SimonButtonUI> _currentButtons = new List<SimonButtonUI>();
        private int _currentNoteIndex = 0;

        public bool IsActive;
        public bool IsPlayer;

        #region Unity events

        private void Awake()
        {
            SimonInputListener.OnInputDetected += OnInputDetected;
            GameEvents.OnSimonEnd += OnSimonEnd;
        }

        private void OnDestroy()
        {
            SimonInputListener.OnInputDetected -= OnInputDetected;
            GameEvents.OnSimonEnd -= OnSimonEnd;
        }

        #endregion

        private void OnInputDetected(ButtonType button)
        {
            if (IsActive)
            {
                bool success = Evaluate(button);
                int errors = 0;

                if (success)
                {
                    // TODO
                    GameEvents.OnButtonSuccess?.Invoke();
                }
                else
                {
                    // TODO
                    GameEvents.OnButtonFailed?.Invoke();
                    errors++;
                }

                if (_currentNoteIndex >= _currentButtons.Count)
                {
                    // TODO
                    GameEvents.OnSheetCompled?.Invoke(errors);
                }
                else
                {
                    _currentNoteIndex++;
                }
            }
        }

        private void OnSimonEnd()
        {
            ClearButtons();
        }

        private void DisplayHUD()
        {
            _hud.DOFade(1f, _secondsFade).SetEase(Ease.Linear).Play();
        }

        private void HideHUD()
        {
            _hud.DOFade(0f, _secondsFade).SetEase(Ease.Linear).Play();
        }

        public void LaunchArea()
        {
            IsActive = true;
            LoadSheet();
        }

        private void LoadSheet()
        {
            var sheet = SimonManager.Instance.GenerateSheet();

            foreach (var note in sheet)
            {
                var item = SimplePool.Spawn(_buttonPrefab, _content);
                item.Setup(note);
                _currentButtons.Add(item);
            }

            _currentNoteIndex = 0;
        }

        private bool Evaluate(ButtonType input)
        {
            return _currentButtons[_currentNoteIndex].Evaluate(input);
        }

        private void ClearButtons()
        {
            foreach(var button in _currentButtons)
            {
                SimplePool.Despawn(button.gameObject);
            }

            _currentButtons.Clear();
        }
    }
}