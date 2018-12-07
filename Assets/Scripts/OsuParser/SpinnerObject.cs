using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsuParser
{
    /* Functional Requirement 
     * ID: 8.1-1
     * Description: The player must be able to view incoming notes.
     *
     * Class for spinner objetcs (inherited from generic hit object)
     * These notes have yet to be added. */
    class SpinnerObject : HitObject
    {
        public float EndTime;

        public SpinnerObject(int x, int y, float startTime, float endTime)
        {
            Position = new Point(x, y);
            StartTime = startTime;
            EndTime = endTime;
            HitObjectType = HitObjectType.Spinner;
        }

        public float EndTimeInMs(float beatLength, float sliderMultiplier)
        {
            return EndTime;
        }

        public float EndTimeInBeats(float beatLength)
        {
            return EndTime / beatLength;
        }
    }
}
