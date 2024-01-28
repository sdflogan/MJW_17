
using MJW.Game;
using MJW.Utils;

namespace MJW.Instruments
{
    public class InstrumentManager : Singleton<InstrumentManager>
    {
        #region Inspector

        

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

        public void OnSheetCompleted(int errors, InstrumentType instrument)
        {

        }

        #endregion
    }
}