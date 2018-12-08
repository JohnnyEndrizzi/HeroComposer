using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* Used for the Osu Parser */
namespace OsuParser
{
    /* Types of notes (only circle and slider will be utilized for now) */
    [Flags]
    public enum HitObjectType
    {
        None = 0,
        Circle = (1 << 0),
        Slider = (1 << 1),
        NewCombo = (1 << 2),
        Spinner = (1 << 3)
    }

    /* Types of hold notes (only Linear will be uitilize for now) */
    public enum SliderType
    {
        Linear = 0,
        PSpline = 1,
        Bezier = 2,
        CSpline = 3
    }
}

/* Used for the core gameplay */
namespace Game
{
    /* Will be used for ticking the metronome */
    public enum BeatDivisions
    {
        Quarter = 1,
        Eighth = 2,
        Sixteenth = 4
    }
}
