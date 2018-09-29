using ProjectValkyrie.Components;
using ProjectValkyrie.Entities;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectValkyrie.Managers
{
    class PhysicsManager
    {
        private readonly Dictionary<long, PhysicsComponent> physics;

        public PhysicsManager()
        {
            physics = new Dictionary<long, PhysicsComponent>();
        }

        public void Add(long id, PhysicsComponent p)
        {
            physics.Add(id, p);
        }

        public void Update(long id, GameTime t)
        {
            //physics[id].Update(t);
            // Physics check should also call GameEntity.OnEvent(e) if a physics object causes a trigger
            // Use GameEntity.TriggerType to determine if/when to trigger during physics update
        }
    }
}
