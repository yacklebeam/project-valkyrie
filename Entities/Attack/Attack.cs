using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities.Attack
{
    class BasicAttack : GameEntity
    {
        private int damage = 0;

        public BasicAttack()
        {
            Type = EntityType.ZONE;
        }

        public int Damage { get => damage; set => damage = value; }

        public override void OnEvent(long id)
        {
            GameEntity ge = GameSession.Instance.EntityManager.Get(id);
            ge.SubtractHealth(damage);
        }

        public override void OnUpdate(GameTime t)
        {
            // Basic Attacks only last one frame (tick)
            // Physics update happens before Entity, so the first time this gets called, kill the entity
            GameSession.Instance.EntityManager.Delete(Id);
            GameSession.Instance.PhysicsManager.Delete(PhysicsId);
            GameSession.Instance.RenderManager.Delete(RenderId);
        }
    }
}
