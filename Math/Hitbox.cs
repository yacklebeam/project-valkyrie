using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectValkyrie.Math
{
    class Hitbox
    {
        private List<Vector2> pointOffsets;

        public Hitbox()
        {
            pointOffsets = new List<Vector2>();
        }

        public List<Vector2> PointOffsets { get => pointOffsets; set => pointOffsets = value; }

        public void Translate(Vector2 v)
        {
            for(int i = 0; i < pointOffsets.Count; ++i)
            {
                pointOffsets[i] = pointOffsets[i] + v;
            }
        }
    }
}
