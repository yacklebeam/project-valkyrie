using Microsoft.Xna.Framework;
using ValhallaEngine.Components;
using ProjectValkyrie.Entities.Attack;
using ValhallaEngine.Entities;
using ValhallaEngine.Math;

namespace ProjectValkyrie.Entities
{
    class Goblin : GameEntity
    {
        private double cooldown;

        public Goblin(long id) : base(id)
        {
            MaxHealth = 15;
            Health = 15;
            Speed = 5.0f;

            cooldown = 1.0;

            Type = EntityType.ENEMY;
        }

        public override void OnEvent(long id)
        {
        }

        public override void OnUpdate(GameTime t)
        {
            if(Health <= 0)
            {
                GameSession.Instance.EntityManager.Delete(Id);
                GameSession.Instance.PhysicsManager.Delete(Id);
                GameSession.Instance.RenderManager.Delete(Id);
                return;
            }

            Vector2 targetPos = GameSession.Instance.PhysicsManager.Get(GameSession.Instance.EntityManager.PlayerId).Position;
            if(cooldown > 0.0) cooldown -= t.ElapsedGameTime.TotalSeconds;

            if (MathUtils.Distance(targetPos, GameSession.Instance.PhysicsManager.Get(Id).Position) < 1.5f)
            {// Attack
                GameSession.Instance.PhysicsManager.Get(Id).Velocity = new Vector2(0.0f, 0.0f);
                if(cooldown <= 0.0) DoAttack();
            }
            else
            {
                Vector2 targetSpeed = Speed * (targetPos - GameSession.Instance.PhysicsManager.Get(Id).Position);

                if (targetSpeed.Length() > Speed)
                {
                    targetSpeed.Normalize();
                    targetSpeed *= Speed;
                }

                GameSession.Instance.PhysicsManager.Get(Id).Velocity = targetSpeed;
            }
        }

        private void DoAttack()
        {
            cooldown = 1.0;
            BasicAttack attack = new BasicAttack(GameSession.NextID);
            attack.Damage = 5;
            PhysicsComponent physics = new PhysicsComponent(attack.Id);
            physics.Type = PhysicsComponent.PhysicsType.INTERSECT;
            physics.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 3, 3);
            physics.Position = GameSession.Instance.PhysicsManager.Get(Id).Position;
            GameSession.Instance.PhysicsManager.Add(physics);
            GameSession.Instance.EntityManager.Add(attack);
        }
    }
}
