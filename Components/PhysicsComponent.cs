using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ProjectValkyrie.Components
{
    class PhysicsComponent
    {
        enum PhysicsType {
            
        }


        private Vector2 postion;
        private Vector2 velocity;
        private List<Vector2> hitbox;
        private long id;
        private Vector2 minBoundingBox;
        private Vector2 maxBoundingBox;

        private bool minBBSet = false;
        private bool maxBBSet = false;

        public PhysicsComponent()
        { }

        public void Update(GameTime t)
        {
            postion += velocity * (float)t.ElapsedGameTime.TotalSeconds;
        }

        public long Id { get => id; set => id = value; }
        internal Vector2 Postion { get => postion; set => postion = value; }
        internal Vector2 Velocity { get => velocity; set => velocity = value; }
        public List<Vector2> Hitbox { get => hitbox; set => hitbox = value; }

        public Vector2 MinBoundingBox
        {
            get
            {
                if(minBBSet)
                {
                    return minBoundingBox;
                }
                else
                {
                    minBoundingBox = new Vector2(int.MaxValue, int.MaxValue);
                    foreach(Vector2 v in hitbox)
                    {
                        minBoundingBox.X = System.Math.Min(v.X, minBoundingBox.X);
                        minBoundingBox.Y = System.Math.Min(v.Y, minBoundingBox.Y);
                    }
                    minBBSet = true;
                    return minBoundingBox;
                }
            }
        }

        public Vector2 MaxBoundingBox
        {
            get
            {
                if (maxBBSet)
                {
                    return maxBoundingBox;
                }
                else
                {
                    maxBoundingBox = new Vector2(int.MinValue, int.MinValue);
                    foreach (Vector2 v in hitbox)
                    {
                        maxBoundingBox.X = System.Math.Max(v.X, maxBoundingBox.X);
                        maxBoundingBox.Y = System.Math.Max(v.Y, maxBoundingBox.Y);
                    }
                    maxBBSet = true;
                    return maxBoundingBox;
                }
            }
        }
    }
}
