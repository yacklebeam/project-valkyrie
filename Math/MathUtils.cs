using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ProjectValkyrie.Math
{
    static class MathUtils
    {
        public static bool Intersect(Vector2 aMin, Vector2 aMax, Vector2 bMin, Vector2 bMax)
        {
            if (aMax.X < bMin.X || aMax.Y < bMin.Y) return false;
            if (bMax.X < aMin.X || bMax.Y < aMin.Y) return false;
            return true;
        }

        public static Vector2 MidPoint(Vector2 a, Vector2 b)
        {
            Vector2 r = new Vector2();
            r.X = (a.X + b.X) / 2.0f;
            r.Y = (a.Y + b.Y) / 2.0f;

            return r;
        }

        public static Hitbox GetRectangleHitbox(Vector2 direction, float length, float width)
        {
            float w = width / 2.0f;
            float l = length / 2.0f;

            direction.Normalize();

            Hitbox h = new Hitbox();
            List<Vector2> hitbox = new List<Vector2>();

            float xMin = -l;
            float yMin = -w;
            float xMax = l;
            float yMax = w;

            hitbox.Add(new Vector2(xMin, yMin));
            hitbox.Add(new Vector2(xMax, yMin));
            hitbox.Add(new Vector2(xMax, yMax));
            hitbox.Add(new Vector2(xMin, yMax));

            h.PointOffsets = hitbox;

            return h;
        }
    }
}
