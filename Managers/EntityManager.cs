using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectValkyrie.Entities.Base;

namespace ProjectValkyrie.Managers
{
    class EntityManager
    {
        private readonly Dictionary<long, GameEntity> entites;
        private long currentId;

        public EntityManager()
        {
            currentId = 0;
            entites = new Dictionary<long, GameEntity>();
        }

        public GameEntity Get(long id)
        {
            return entites[id];
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
                e.Update(t); // Entity Update
            }
        }
        
        public void Render(SpriteBatch sb)
        {
            foreach (GameEntity e in entites.Values)
            {
                e.Render(sb); // Entity Render
            }
        }

        public void AddEntity(GameEntity e)
        {
            entites.Add(e.Id, e);
        }
    }
}
