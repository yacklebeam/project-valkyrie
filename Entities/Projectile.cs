using Microsoft.Xna.Framework;
using ValhallaEngine.Entity;
using ValhallaEngine.Component;
using ValhallaEngine.Math;

namespace ProjectValkyrie.Entities
{
    class BasicProjectile : GameEntity
    {
        private int damage;
        private EntityType entityFaction;

        public int Damage { get => damage; set => damage = value; }
        public EntityType EntityFaction { get => entityFaction; set => entityFaction = value; }

        public BasicProjectile(long id) : base(id)
        {
            damage = 0;
            entityFaction = EntityType.NONE;
        }

        public override void OnUpdate(GameTime t)
        {
        }

        public override void OnEvent(long id)
        {
            GameEntity ge = GameSession.Instance.EntityManager.Get(id);
            if (ge.Type == EntityType.PLAYER && entityFaction == EntityType.ENEMY ||
                ge.Type == EntityType.ENEMY && entityFaction == EntityType.PLAYER ||
                entityFaction == EntityType.NONE)
            {
                ge.SubtractHealth(damage);
            }
        }
    }

    class SuperMissile : BasicProjectile
    {
        private float cooldown;
        public SuperMissile(long id) : base(id)
        {
            Damage = 10;
            cooldown = 0.5f;
        }

        public override void OnUpdate(GameTime t)
        {
            base.OnUpdate(t);
            cooldown -= (float)t.ElapsedGameTime.TotalSeconds;
            if(cooldown <= 0.0f)
            {
                CreateNewMissile(Vector2.Transform(GameSession.Instance.PhysicsManager.Get(Id).Velocity, Matrix.CreateRotationZ(1)));
                CreateNewMissile(Vector2.Transform(GameSession.Instance.PhysicsManager.Get(Id).Velocity, Matrix.CreateRotationZ(-1)));
                cooldown += 0.5f;
            }
        }

        private void CreateNewMissile(Vector2 vel)
        {
            BasicProjectile test = new BasicProjectile(GameSession.NextID);
            PhysicsComponent testPc = new PhysicsComponent(test.Id);
            testPc.Type = PhysicsComponent.PhysicsType.NONE;
            testPc.Position = GameSession.Instance.PhysicsManager.Get(Id).Position;
            testPc.Velocity = vel;
            testPc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 0.5f, 0.5f);
            GameSession.Instance.PhysicsManager.Add(testPc);
            RenderComponent testRc = new RenderComponent(test.Id);
            testRc.SpriteID = 2;
            GameSession.Instance.RenderManager.Add(testRc);
            GameSession.Instance.EntityManager.Add(test);
        }
    }

}
