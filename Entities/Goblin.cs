using Microsoft.Xna.Framework;
using ValhallaEngine.Components;
using ProjectValkyrie.Entities.Attack;
using ValhallaEngine.Entities;
using ValhallaEngine.Managers;
using ValhallaEngine.Math;

namespace ProjectValkyrie.Entities
{
    class Goblin : GameEntity
    {
        private double cooldown;

        public Goblin() : base()
        {
            MaxHealth = 5;
            Health = 5;
            Speed = 5.0f;

            cooldown = 1.0;

            Type = EntityType.ENEMY;
        }

        public override void OnEvent(long id)
        {
        }

        public override void OnUpdate(GameTime t)
        {
            Vector2 targetPos = GameSession.Instance.EntityManager.GetPlayerPosition();
            if(cooldown > 0.0) cooldown -= t.ElapsedGameTime.TotalSeconds;

            if (MathUtils.Distance(targetPos, GameSession.Instance.PhysicsManager.Get(PhysicsId).Position) < 1.5f)
            {// Attack
                GameSession.Instance.PhysicsManager.Get(PhysicsId).Velocity = new Vector2(0.0f, 0.0f);
                if(cooldown <= 0.0) DoAttack();
            }
            else
            {

                Vector2 targetSpeed = Speed * (targetPos - GameSession.Instance.PhysicsManager.Get(PhysicsId).Position);

                if (targetSpeed.Length() > Speed)
                {
                    targetSpeed.Normalize();
                    targetSpeed *= Speed;
                }

                GameSession.Instance.PhysicsManager.Get(PhysicsId).Velocity = targetSpeed;
            }
        }

        private void DoAttack()
        {
            cooldown = 1.0;
            BasicAttack attack = new BasicAttack();
            attack.Damage = 5;
            PhysicsComponent physics = new PhysicsComponent(attack.Id);
            physics.Type = PhysicsComponent.PhysicsType.INTERSECT;
            physics.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 3, 3);
            physics.Position = GameSession.Instance.PhysicsManager.Get(PhysicsId).Position;
            attack.PhysicsId = GameSession.Instance.PhysicsManager.Add(physics);
            GameSession.Instance.EntityManager.AddEntity(attack);
        }
    }
}
