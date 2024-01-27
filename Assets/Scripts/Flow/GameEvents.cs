
using System;

namespace MJW.Game
{
    public static class GameEvents
    {
        public static Action OnGameStarted;
        public static Action OnGameReady;

        public static Action OnSimonStart;
        public static Action OnSimonEnd;

        public static Action OnButtonFailed;
        public static Action OnButtonSuccess;

        public static Action<int> OnSheetCompled;

        public static Action OnStickSuccess;
        public static Action OnStickFailed;

        public static Action OnBurnt;
    }
}