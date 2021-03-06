﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsuParser
{
    /* Functional Requirement 
     * ID: 8.1-1
     * Description: The player must be able to view incoming notes.
     *
     * Class for slider/hold objetcs (inherited from generic hit object) */
    class SliderObject : HitObject
    {
        public float PixelLength { get; private set; }

        public SliderObject(int x, int y, float startTime, float pixelLength)
        {
            Position = new Point(x, y);
            StartTime = startTime;
            PixelLength = pixelLength;
            HitObjectType = HitObjectType.Slider;
            
        }

        public float EndTimeInMs(float beatLength, float sliderMultiplier)
        {
            return (PixelLength / (100 * sliderMultiplier)) * beatLength;
        }

        public float EndTimeInS(float beatLength, float sliderMultiplier)
        {
            return (PixelLength / (100 * sliderMultiplier)) * (beatLength/1000);
        }

        public float EndTimeInBeats(float beatLength, float sliderMultiplier)
        {
            return PixelLength / (100 * sliderMultiplier);
        }
    }
}
