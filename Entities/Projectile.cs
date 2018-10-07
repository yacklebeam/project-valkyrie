using Microsoft.Xna.Framework;
using ValhallaEngine.Entities;
using ValhallaEngine.Components;
using ValhallaEngine.Math;

namespace ProjectValkyrie.Entities
{
    class Projectile
    {
        private float damage;

        public float Damage { get => damage; set => damage = value; }
        public EntityType EntityFaction { get => entityFaction; set => entityFaction = value; }

        private EntityType entityFaction;

        public Projectile(long id) : base(id)
        {
            damage = 0.0f;



        }

        public override void OnEvent(long id)
    }
}
