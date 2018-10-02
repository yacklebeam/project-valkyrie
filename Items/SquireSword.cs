using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Items
{
    class SquireSword : Base.GameItem
    {
        public override void OnUsePrimary(GameEntity e)
        {
            Vector2 direction = GameSession.Instance.PhysicsManager.Get(e.PhysicsId).Direction;
            SquireSwordPrimaryAttack attack = new SquireSwordPrimaryAttack(direction);
        }

        public override void OnUseSecondary(GameEntity e)
        {
        }

        internal class SquireSwordPrimaryAttack : GameEntity
        {
            private readonly int damage = 5;
            public SquireSwordPrimaryAttack(Vector2 direction)
            {
                // Create new physics object for the hitbox
                Components.PhysicsComponent pc = new Components.PhysicsComponent(Id);
                pc.Direction = direction;
                pc.Hitbox = Math.MathUtils.GetRectangleHitbox(pc.Direction, 10.0f, 10.0f);
                pc.Hitbox.Translate(pc.Direction * 10.0f); // Move forward 10 units (we don't need the hitbox AROUND the sword, but in front of it
                PhysicsId = GameSession.Instance.PhysicsManager.Add(pc);
            }

            public override void OnEvent(long id)
            {
                GameSession.Instance.EntityManager.Get(id).SubtractHealth(damage);
            }

            public override void OnUpdate(GameTime t)
            { }
        }
    }
}