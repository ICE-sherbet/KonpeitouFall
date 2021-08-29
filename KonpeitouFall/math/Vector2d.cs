using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oicf.math
{
    class Vector2d
    {
        public float x, y;
        public Vector2d(float x,float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2d(Vector2d other)
        {
            this.x = other.x;
            this.y = other.y;
        }
        public static Vector2d up { get { return new Vector2d(0f, 1f); } }

    }
}
