
using System;

namespace MJW.Simon
{
    [Serializable]
    public struct SheetDifficulty
    {
        public Difficulty Diff;
        public int MinNotes;
        public int MaxNotes;
    }

    public enum Difficulty { Undefined, Easy, Medium, Hard }
}