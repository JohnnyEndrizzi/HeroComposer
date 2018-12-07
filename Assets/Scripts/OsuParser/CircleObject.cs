using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsuParser
{
    /* Class for circle objetcs (inherited from generic hit object) */
    class CircleObject : HitObject
    {
        public CircleObject(int x, int y, float startTime)
        {
            Position = new Point(x, y);
            StartTime = startTime;
            HitObjectType = HitObjectType.Circle;
        }
    }
}
