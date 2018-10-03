using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectValkyrie.Entities.Base;
using ProjectValkyrie.Managers;
using ProjectValkyrie.Math;
using System.Collections.Generic;

namespace ProjectValkyrie.Components
{
    class PhysicsComponent
    {
        public enum PhysicsType {
            NONE,
            COLLIDE,
            INTERSECT
        }

        private Vector2 direction;
        private Vector2 position;
        private Vector2 velocity;
        private Hitbox hitbox = null; // A list of offsets from position
        private long id;
        private Vector2 minBoundingBox;
        private Vector2 maxBoundingBox;
        private long entityId;
        private PhysicsType type = PhysicsType.NONE;

        public PhysicsComponent(long entityId)
        {
            this.entityId = entityId;
        }

        public void Update(GameTime t)
        {
            Vector2 originalPos = position;
            position += velocity * (float)t.ElapsedGameTime.TotalSeconds;
            // If necessary, call TriggerEvent();
            List<long> pcs = GameSession.Instance.PhysicsManager.GetIntersections(Id);

            foreach(long otherId in pcs)
            {
                PhysicsComponent p = GameSession.Instance.PhysicsManager.Get(otherId);
                if(this.Type == PhysicsType.COLLIDE && p.Type == PhysicsType.COLLIDE)
                {// We need to adjust our movement
                    position = originalPos; // For now, just prevent the movement.  Eventually, calculate the pen distance

                    // Also, trigger our event
                    TriggerEvent(otherId);
                }
                else if(this.Type == PhysicsType.INTERSECT)
                {// We intersected the object. trigger our event
                    TriggerEvent(otherId);
                }
            }
        }

        private void TriggerEvent(long id)
        {
            GameSession.Instance.EntityManager.Get(entityId).OnEvent(id);
        }

        public long Id { get => id; set => id = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public Hitbox Hitbox { get => hitbox; set => hitbox = value; }
        public PhysicsType Type { get => type; set => type = value; }


        public Vector2 MinBoundingBox
        {
            get
            {
                minBoundingBox = new Vector2(int.MaxValue, int.MaxValue);
                foreach(Vector2 v in hitbox.PointOffsets)
                {
                    minBoundingBox.X = System.Math.Min(position.X + v.X, minBoundingBox.X);
                    minBoundingBox.Y = System.Math.Min(position.Y + v.Y, minBoundingBox.Y);
                }
                return minBoundingBox;
            }
        }

        public Vector2 MaxBoundingBox
        {
            get
            {
                maxBoundingBox = new Vector2(int.MinValue, int.MinValue);
                foreach (Vector2 v in hitbox.PointOffsets)
                {
                    maxBoundingBox.X = System.Math.Max(position.X + v.X, maxBoundingBox.X);
                    maxBoundingBox.Y = System.Math.Max(position.Y + v.Y, maxBoundingBox.Y);
                }
                return maxBoundingBox;
            }
        }

        public void Render(SpriteBatch sb)
        {
            if(hitbox != null)
            {
                Vector2 minPixels = GameSession.Instance.PhysicsManager.ConvertToScreenCoordinates(MinBoundingBox);
                Vector2 maxPixels = GameSession.Instance.PhysicsManager.ConvertToScreenCoordinates(MaxBoundingBox);

                Rectangle r = new Rectangle((int)minPixels.X, (int)minPixels.Y, (int)(maxPixels.X - minPixels.X), (int)(maxPixels.Y - minPixels.Y));

                sb.Draw(GameSession.Instance.AssetManager.getTexture("hitbox"), r, Color.White);
            }
        }
    }
}
