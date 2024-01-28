
using DG.Tweening;
using MJW.Game;
using MJW.Instruments;
using MJW.MyTime;
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
        [SerializeField] private GameObject _callToAction;

        [Header("Config")]
        [SerializeField] private float _secondsFade;
        [SerializeField] private InstrumentType _instrument;

        private List<SimonButtonUI> _currentButtons = new List<SimonButtonUI>();
        private int _currentNoteIndex = 0;

        public bool IsActive;
        public bool CanInteract;

        #region Unity events

        private void Awake()
        {
            _hud.alpha = 0;
            _callToAction.SetActive(false);

            SimonInputListener.OnInputDetected += OnInputDetected;
            GameEvents.OnSimonEnd += OnSimonEnd;
        }

        private void OnDestroy()
        {
            SimonInputListener.OnInputDetected -= OnInputDetected;
            GameEvents.OnSimonEnd -= OnSimonEnd;
        }

        #endregion

        private int _lastPlayerCount = 0;

        public void UpdatePlayers(int count)
        {
            _lastPlayerCount = count;
            if (IsActive && count >= 2)
            {
                DisplayHUD();
            }
            else
            {
                HideHUD();
            }
        }

        private void OnInputDetected(ButtonType button)
        {
            if (CanInteract)
            {
                bool success = Evaluate(button);
                int errors = 0;

                if (success)
                {
                    // TODO
                    GameEvents.OnNoteSuccess?.Invoke(_instrument);
                }
                else
                {
                    // TODO
                    GameEvents.OnNoteFailed?.Invoke(_instrument);
                    errors++;
                }

                if (_currentNoteIndex + 1 >= _currentButtons.Count)
                {
                    // TODO
                    GameEvents.OnSheetCompled?.Invoke(errors, _instrument, (int) TimeManager.Instance.RemainingSeconds);
                }
                else
                {
                    _currentNoteIndex++;
                }
            }
        }

        private void OnSimonEnd(int errors)
        {
            HideHUD(true);
            IsActive = false;
        }

        private void DisplayHUD()
        {
            CanInteract = true;

            if (_hud.alpha == 1) return;

            _hud.DOKill(true);
            _hud.DOFade(1f, _secondsFade).SetEase(Ease.Linear).Play();
            _callToAction.SetActive(false);
        }

        private void HideHUD(bool clear = false)
        {
            CanInteract = false;

            if (_hud.alpha == 0)
            {
                if (clear) ClearButtons();
                return;
            }

            _hud.DOKill(true);

            _hud.DOFade(0f, _secondsFade).
                SetEase(Ease.Linear).
                OnComplete(
                () =>
                {
                    if (clear) ClearButtons();
                }).Play();
        }

        public void LaunchArea()
        {
            IsActive = true;
            ClearButtons();
            LoadSheet();
            _callToAction.SetActive(true);
            UpdatePlayers(_lastPlayerCount);
        }

        private void LoadSheet()
        {
            var sheet = SimonManager.Instance.GenerateSheet();

            Debug.LogError("Loaded new sheet: " + sheet.Count + " Buttons count: " + _currentButtons.Count);

            foreach (var note in sheet)
            {
                var item = SimplePool.Spawn(_buttonPrefab, _content);
                item.Setup(note);
                _currentButtons.Add(item);
            }

            _currentNoteIndex = 0;

            GameEvents.OnSheetGenerated?.Invoke(sheet.Count, _instrument);
        }

        private bool Evaluate(ButtonType input)
        {
            return _currentButtons[_currentNoteIndex].Evaluate(input);
        }

        private void ClearButtons()
        {
            _callToAction.SetActive(false);

            foreach(var button in _currentButtons)
            {
                button.transform.parent = null;
                SimplePool.Despawn(button.gameObject);
            }

            _currentButtons.Clear();
        }
    }
}