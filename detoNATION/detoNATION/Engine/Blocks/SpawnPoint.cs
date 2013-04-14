using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class SpawnPoint
    {
        private Point point;
        private DateTime time;

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        public SpawnPoint(Point p)
        {
            point = p;
        }
    }
}
