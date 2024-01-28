
using MJW.Audio;
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
        private int _currentNoteIndex;
        private InstrumentSheet _currentSheet;

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
            if (_currentNoteIndex < _currentSheet.Notes.Count)
            {
                AudioManager.Instance.PlaySFX(_currentSheet.Notes[_currentNoteIndex]);
            }
            else if (_currentNoteIndex-1 < _currentSheet.Notes.Count)
            {
                AudioManager.Instance.PlaySFX(_currentSheet.Notes[_currentNoteIndex-1]);
            }

            _currentNoteIndex++;
        }

        public void OnNoteFailed(InstrumentType instrument)
        {
            AudioManager.Instance.PlaySFX(GetFailSound(instrument));
            _currentNoteIndex++;
        }

        public void OnSheetGenerated(int notes, InstrumentType instrument)
        {
            _currentNotesAmount = notes;
            _currentInstrument = instrument;
            _currentNoteIndex = 0;
            _currentSheet = Find(instrument, notes);
        }

        public void OnSheetCompleted(int errors, InstrumentType instrument)
        {
            AudioManager.Instance.PlaySFX(GetOkSound(instrument));
        }

        #endregion

        private InstrumentSheet Find(InstrumentType instrument, int Notes)
        {
            var sheets = new List<InstrumentSheet>();

            switch (instrument)
            {
                case InstrumentType.Banjo:
                    foreach(var banjo in _banjoSheets)
                    {
                        if (banjo.Notes.Count == Notes)
                        {
                            sheets.Add(banjo);
                        }
                    }
                    break;

                case InstrumentType.Tambor:
                    foreach (var perc in _percSheets)
                    {
                        if (perc.Notes.Count == Notes)
                        {
                            sheets.Add(perc);
                        }
                    }
                    break;

                case InstrumentType.Trompeta:
                    foreach (var xyl in _xylSheets)
                    {
                        if (xyl.Notes.Count == Notes)
                        {
                            sheets.Add(xyl);
                        }
                    }
                    break;
            }

            if (sheets.Count > 0)
            {
                return sheets[Random.Range(0, sheets.Count)];
            }

            return new InstrumentSheet();
        }

        private SoundType GetOkSound(InstrumentType type)
        {
            switch (type)
            {
                case InstrumentType.Banjo:
                    return SoundType.banjoOk;

                case InstrumentType.Tambor:
                    return SoundType.percOk;

                case InstrumentType.Trompeta:
                    return SoundType.xylOk;
            }

            return SoundType.Undefined;
        }

        private SoundType GetFailSound(InstrumentType type)
        {
            switch (type)
            {
                case InstrumentType.Banjo:
                    return SoundType.banjoFail;

                case InstrumentType.Tambor:
                    return SoundType.percFail;

                case InstrumentType.Trompeta:
                    return SoundType.xylFail;
            }

            return SoundType.Undefined;
        }
    }
}