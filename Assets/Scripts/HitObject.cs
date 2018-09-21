using System;

namespace OsuParser
{
    public abstract class HitObject
    {
        public struct Point
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        protected Point Position;
        protected float StartTime { get; set; }
        protected HitObjectType HitObjectType { get; set; }

        //Return the time in ms the note should be hit
        public float StartTimeInMiliseconds()
        {
            return StartTime;
        }

        //Return the time in s the note should be hit
        public float StartTimeInSeconds()
        {
            return StartTime / 1000;
        }

        //Return the beat the note should be hit
        public float StartTimeInBeats(decimal msPerBeat)
        {
            return ClampBeat((float)(StartTime/(float)msPerBeat));
        }

        //Clamp the beat to a quarter, eighth, or sixteenth note in case float value is off
        private float ClampBeat(float beat)
        {
            //Get part before decimal
            float prefix = (float)Math.Truncate(beat);
            //Get part after decimal 
            var x = beat - Math.Truncate(beat);

            //Quarter note
            if (x >= 0.0 && x < 0.125)
            {
                return (prefix + 0.00F);
            }
            //Sixteenth note
            else if (x >= 0.125 && x < 0.375)
            {
                return (prefix + 0.25F);
            }
            //Eighth note
            else if (x >= 0.375 && x < 0.625)
            {
                return (prefix + 0.50F);
            }
            //Sixteenth note
            else if (x >= 0.625 && x < 0.875)
            {
                return (prefix + 0.75F);
            }
            //Quarter note
            else
            {
                return beat;
            }
        }
    }
}