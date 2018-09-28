using ProjectValkyrie.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectValkyrie.Managers
{
    class PhysicsManager
    {
        private readonly Dictionary<long, PhysicsComponent> physics;

        public PhysicsManager()
        {

        }

        public void Add(long id, PhysicsComponent p)
        {
            physics.Add(id, p);
        }

        public void Update(long id, GameTime t)
        {
            physics[id].Update(t);
        }
    }
}
