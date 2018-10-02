using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using ProjectValkyrie.Managers;
using ProjectValkyrie.Math;
using System.Collections.Generic;

namespace ProjectValkyrie.Components
{
    class PhysicsComponent
    {
        enum PhysicsType {
            
        }

        private Vector2 direction;
        private Vector2 postion;
        private Vector2 velocity;
        private Hitbox hitbox; // A list of offsets from position
        private long id;
        private Vector2 minBoundingBox;
        private Vector2 maxBoundingBox;
        private long entityId;

        private bool minBBSet = false;
        private bool maxBBSet = false;

        public PhysicsComponent(long entityId)
        {
            this.entityId = entityId;
        }

        public void Update(GameTime t)
        {
            postion += velocity * (float)t.ElapsedGameTime.TotalSeconds;
            // If necessary, call TriggerEvent();
        }

        private void TriggerEvent(long id)
        {
            GameSession.Instance.EntityManager.Get(entityId).OnEvent(id);
        }

        public long Id { get => id; set => id = value; }
        public Vector2 Postion { get => postion; set => postion = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public Hitbox Hitbox { get => hitbox; set => hitbox = value; }

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
                    foreach(Vector2 v in hitbox.PointOffsets)
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
                    foreach (Vector2 v in hitbox.PointOffsets)
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
