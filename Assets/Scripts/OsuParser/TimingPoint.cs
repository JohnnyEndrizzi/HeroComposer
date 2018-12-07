using System;

namespace OsuParser
{
    /* Class used to keep track of input beats for the provided song */
    public class TimingPoint
    {
        public float Time;
        public decimal TimePerBeat;
        public int TimeSignature = 4;
        public bool FrenzyMode;

        /* Stores the beats */
        public TimingPoint(float time, decimal timePerBeat, int timeSignature, bool frenzyMode)
        {
            Time = time;
            TimePerBeat = timePerBeat;
            TimeSignature = timeSignature;
            FrenzyMode = frenzyMode;
        }
    }
}