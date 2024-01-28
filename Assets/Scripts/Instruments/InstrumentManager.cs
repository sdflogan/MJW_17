using MJW.Audio;
using MJW.Game;
using MJW.Utils;
using System;
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
            GameEvents.OnSheetGenerated += OnSheetGenerated;
            GameEvents.OnSheetCompled += OnSheetCompleted;
        }

        private void OnDestroy()
        {
            GameEvents.OnNoteSuccess -= OnNoteSuccess;
            GameEvents.OnNoteFailed -= OnNoteFailed;
            GameEvents.OnSheetGenerated -= OnSheetGenerated;
            GameEvents.OnSheetCompled -= OnSheetCompleted;
        }

        #endregion

        #region Callbacks 

        public void OnNoteSuccess(InstrumentType instrument)
        {
            try
            {
                if (_currentNoteIndex < _currentSheet.Notes.Count)
                {
                    AudioManager.Instance.PlaySFX(_currentSheet.Notes[_currentNoteIndex]);
                }
                else if (_currentNoteIndex - 1 < _currentSheet.Notes.Count)
                {
                    AudioManager.Instance.PlaySFX(_currentSheet.Notes[_currentNoteIndex - 1]);
                }
                _currentNoteIndex++;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError(instrument);
            }
        }

        public void OnNoteFailed(InstrumentType instrument)
        {
            try
            {
                AudioManager.Instance.PlaySFX(GetFailSound(instrument));
                _currentNoteIndex++;
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError(instrument);
            }
        }

        public void OnSheetGenerated(int notes, InstrumentType instrument)
        {
            try
            {
                _currentNotesAmount = notes;
                _currentInstrument = instrument;
                _currentNoteIndex = 0;
                _currentSheet = Find(instrument, notes);

                if (_currentSheet.Notes == null || _currentSheet.Notes.Count <= 0)
                {
                    Debug.LogError("Failed generating sheet: " + instrument + " - " + notes);
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError(instrument);
            }
        }

        public void OnSheetCompleted(int errors, InstrumentType instrument, int remainingSeconds)
        {
            try
            {
                AudioManager.Instance.PlaySFX(GetOkSound(instrument));
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError(instrument);
            }
        }

        #endregion

        private InstrumentSheet Find(InstrumentType instrument, int notes)
        {
            Debug.LogError("find " + notes);
            var sheets = new List<InstrumentSheet>();

            switch (instrument)
            {
                case InstrumentType.Banjo:
                    foreach(var banjo in _banjoSheets)
                    {
                        if (banjo.Notes.Count == notes)
                        {
                            sheets.Add(banjo);
                        }
                    }
                    break;

                case InstrumentType.Tambor:
                    foreach (var perc in _percSheets)
                    {
                        if (perc.Notes.Count == notes)
                        {
                            sheets.Add(perc);
                        }
                    }
                    break;

                case InstrumentType.Trompeta:
                    foreach (var xyl in _xylSheets)
                    {
                        if (xyl.Notes.Count == notes)
                        {
                            sheets.Add(xyl);
                        }
                    }
                    break;

                default:
                    Debug.LogError("type is undefined");
                    break;
            }

            if (sheets.Count > 0)
            {
                return sheets[UnityEngine.Random.Range(0, sheets.Count)];
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

                default:
                    Debug.LogError("type is undefined");
                    break;
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

                default:
                    Debug.LogError("type is undefined");
                    break;
            }

            return SoundType.Undefined;
        }
    }
}