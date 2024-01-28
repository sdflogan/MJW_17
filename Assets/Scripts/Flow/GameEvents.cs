
using MJW.Instruments;
using MJW.Simon;
using System;

namespace MJW.Game
{
    public static class GameEvents
    {
        public static Action OnGameStarted;
        public static Action OnGameReady;
        public static Action OnGameEnd;
        public static Action OnGameRestart;

        public static Action OnSimonStart;
        public static Action<int> OnSimonEnd;

        public static Action<InstrumentType> OnNoteFailed;
        public static Action<InstrumentType> OnNoteSuccess;

        public static Action<int, InstrumentType> OnSheetGenerated;
        public static Action<int, InstrumentType, int> OnSheetCompled;

        public static Action OnStickSuccess;
        public static Action OnStickFailed;

        public static Action OnBurnt;

        public static Action<int> OnTimeUpdated;
    }
}