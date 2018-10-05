using Microsoft.Xna.Framework;
using ValhallaEngine.Entities;

namespace ProjectValkyrie.Entities.Attack
{
    class BasicAttack : GameEntity
    {
        private int damage = 0;

        public BasicAttack(long id): base(id)
        {
            Type = EntityType.ZONE;
        }

        public int Damage { get => damage; set => damage = value; }

        public override void OnEvent(long id)
        {
            GameEntity ge = GameSession.Instance.EntityManager.Get(id);
            if (ge.Type == EntityType.PLAYER)
            {
                ge.SubtractHealth(5);
            }
        }

        public override void OnUpdate(GameTime t)
        {
            // Basic Attacks only last one frame (tick)
            // Physics update happens before Entity, so the first time this gets called, kill the entity
            GameSession.Instance.EntityManager.Delete(Id);
            GameSession.Instance.PhysicsManager.Delete(Id);
            GameSession.Instance.RenderManager.Delete(Id);
        }
    }

    class HeroBasicAttack : GameEntity
    {
        private int damage = 0;

        public HeroBasicAttack(long id) : base(id)
        {
            Type = EntityType.ZONE;
        }

        public int Damage { get => damage; set => damage = value; }

        public override void OnEvent(long id)
        {
            GameEntity ge = GameSession.Instance.EntityManager.Get(id);
            if (ge.Type == EntityType.ENEMY)
            {
                ge.SubtractHealth(5);
                GameSession.Instance.PhysicsManager.Get(id).Velocity = new Vector2(-5.0f, 0.0f);
            }
        }

        public override void OnUpdate(GameTime t)
        {
            // Basic Attacks only last one frame (tick)
            // Physics update happens before Entity, so the first time this gets called, kill the entity
            GameSession.Instance.EntityManager.Delete(Id);
            GameSession.Instance.PhysicsManager.Delete(Id);
            GameSession.Instance.RenderManager.Delete(Id);
        }
    }
}
