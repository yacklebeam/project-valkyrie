using Microsoft.Xna.Framework;

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
    }
}
