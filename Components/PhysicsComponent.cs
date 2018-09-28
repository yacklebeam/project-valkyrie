using Microsoft.Xna.Framework;

namespace ProjectValkyrie.Components
{
    class PhysicsComponent
    {
        private Vector2 postion;
        private Vector2 velocity;
        private long id;

        public PhysicsComponent()
        {}

        public void Update(GameTime t)
        {
            postion += velocity * t.ElapsedGameTime.Milliseconds;
        }

        public long Id { get => id; set => id = value; }
        internal Vector2 Postion { get => postion; set => postion = value; }
        internal Vector2 Velocity { get => velocity; set => velocity = value; }
    }
}
