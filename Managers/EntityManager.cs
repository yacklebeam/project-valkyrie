﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectValkyrie.Managers
{
    class EntityManager
    {
        private readonly Dictionary<long, Entities.Base.GameEntity> entites;
        private long currentId;

        public EntityManager()
        {
            currentId = 0;
            entites = new Dictionary<long, Entities.Base.GameEntity>();
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
            foreach (Entities.Base.GameEntity e in entites.Values)
            {
                //e.Render(sb, physicsManager); // Entity Render
            }
        }

        public long AddEntity(Entities.Base.GameEntity e)
        {
            long id = GetNextID();
            e.Id = id;
            entites.Add(id, e);

            return id;
        }
    }
}
