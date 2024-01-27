
using MJW.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Simon
{
    public class SimonManager : Singleton<SimonManager>
    {
        [SerializeField] private List<SimonButtonData> _buttons;
        [SerializeField] private List<SheetDifficulty> _difficulty;
        [SerializeField] private List<SimonArea> _areas;
        [SerializeField] private Color _success;
        [SerializeField] private Color _error;

        public Color SuccessColor => _success;
        public Color ErrorColor => _error;

        public Difficulty CurrentDifficulty { get; private set; }

        public void SelectRandomArea()
        {
            _areas[Random.Range(0, _areas.Count - 1)].LaunchArea();
        }

        public List<ButtonType> GenerateSheet()
        {
            var difData = _difficulty.Find(element => element.Diff == CurrentDifficulty);

            int sheetSize = Random.Range(difData.MinNotes, difData.MaxNotes);

            List<ButtonType> buttons = new List<ButtonType>();

            for (int i=0; i<sheetSize; i++)
            {
                ButtonType randomButton = (ButtonType)Random.Range(0, 7);
                buttons.Add(randomButton);
            }

            return buttons;
        }

        public Sprite GetIcon(ButtonType button)
        {
            var data = _buttons.Find(element => element.Type == button);

            return data.Icon;
        }
        
    }
}