using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectValkyrie.Managers
{
    class EntityManager
    {
        private readonly Dictionary<long, Entities.Base.GameEntity> entites;
        private readonly PhysicsManager _pm;
        private readonly RenderManager _rm;
        private long currentId;

        public EntityManager()
        {
            currentId = 0;
            entites = new Dictionary<long, Entities.Base.GameEntity>();
            _pm = new PhysicsManager();
            _rm = new RenderManager();
        }

        public long GetNextID()
        {
            long Id = currentId;
            currentId++;
            return Id;
        }

        public void Update(GameTime t)
        {
            foreach(Entities.Base.GameEntity e in entites.Values)
            {
                if(e.HasPhysics) _pm.Update(e.Id, t); // Physics Update, may generate OnEvent() calls
                e.Update(t); // Entity Update
            }
        }
        
        public void Render()
        {
            
        }

        public void AddEntity(long id, Entities.Base.GameEntity e, Components.PhysicsComponent p)
        {
            e.Id = id;
            p.Id = id;

            entites.Add(id, e);
            _pm.Add(id, p);
        }

        public void AddEntity(long id, Entities.Base.GameEntity e)
        {
            e.Id = id;
            entites.Add(id, e);
        }
    }
}
