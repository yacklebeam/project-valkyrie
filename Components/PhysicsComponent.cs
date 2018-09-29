using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ProjectValkyrie.Components
{
    class PhysicsComponent
    {
        private Vector2 postion;
        private Vector2 velocity;
        private List<Vector2> hitbox;
        private long id;

        public PhysicsComponent()
        {}

        public void Update(GameTime t)
        {
            postion += velocity * t.ElapsedGameTime.Milliseconds;
            // Resolve collisions or intersections.
            // Call OnEvent() if necessary
        }

        public long Id { get => id; set => id = value; }
        internal Vector2 Postion { get => postion; set => postion = value; }
        internal Vector2 Velocity { get => velocity; set => velocity = value; }
        public List<Vector2> Hitbox { get => hitbox; set => hitbox = value; }
    }
}
