
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

        private List<SimonButtonUI> _currentButtons = new List<SimonButtonUI>();
        private int _currentNoteIndex = 0;

        public bool IsActive;

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

        private bool Check(ButtonType input)
        {
            return _currentButtons[_currentNoteIndex].Check(input);
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