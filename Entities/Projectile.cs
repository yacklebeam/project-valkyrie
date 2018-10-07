using Microsoft.Xna.Framework;
using ValhallaEngine.Entity;
using ValhallaEngine.Component;
using ValhallaEngine.Math;

namespace ProjectValkyrie.Entities
{
    class Projectile : GameEntity
    {
        private float damage;
        private EntityType entityFaction;

        public float Damage { get => damage; set => damage = value; }
        public EntityType EntityFaction { get => entityFaction; set => entityFaction = value; }

        public Projectile(long id) : base(id)
        {
            damage = 0.0f;
        }

        public override void OnUpdate(GameTime t)
        {
        }

        public override void OnEvent(long id)
        {
        }
    }
}
