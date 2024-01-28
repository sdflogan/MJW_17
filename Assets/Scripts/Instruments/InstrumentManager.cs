
using MJW.Game;
using MJW.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Instruments
{
    public class InstrumentManager : Singleton<InstrumentManager>
    {
        #region Inspector

        [SerializeField] private List<InstrumentSheet> _banjoSheets;
        [SerializeField] private List<InstrumentSheet> _percSheets;
        [SerializeField] private List<InstrumentSheet> _xylSheets;


        #endregion

        #region Private properties

        private InstrumentType _currentInstrument;
        private int _currentNotesAmount;

        #endregion

        #region Unity events

        private void Awake()
        {
            GameEvents.OnNoteSuccess += OnNoteSuccess;
            GameEvents.OnNoteFailed += OnNoteFailed;
            GameEvents.OnSheetCompled += OnSheetCompleted;
        }

        private void OnDestroy()
        {
            GameEvents.OnNoteSuccess -= OnNoteSuccess;
            GameEvents.OnNoteFailed -= OnNoteFailed;
            GameEvents.OnSheetCompled -= OnSheetCompleted;
        }

        #endregion

        #region Callbacks 

        public void OnNoteSuccess(InstrumentType instrument)
        {

        }

        public void OnNoteFailed(InstrumentType instrument)
        {

        }

        public void OnSheetGenerated(int notes, InstrumentType instrument)
        {

        }

        public void OnSheetCompleted(int errors, InstrumentType instrument)
        {

        }

        #endregion
    }
}