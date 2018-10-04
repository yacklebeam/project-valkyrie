using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;

namespace ProjectValkyrie.Managers
{
    class EntityManager
    {
        private readonly Dictionary<long, GameEntity> entites;
        private readonly Dictionary<long, GameEntity> newEntities;
        private readonly List<long> deletedEntities;
        private long currentId;
        private long playerId;

        public long PlayerId { get => playerId; set => playerId = value; }

        public EntityManager()
        {
            playerId = -1;
            currentId = 0;
            newEntities = new Dictionary<long, GameEntity>();
            entites = new Dictionary<long, GameEntity>();
            deletedEntities = new List<long>();
        }

        public GameEntity Get(long id)
        {
            try
            {
                return entites[id];
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
        }

        public long GetNextID()
        {
            long Id = currentId;
            currentId++;
            return Id;
        }

        public void Update(GameTime t)
        {
            foreach(GameEntity e in entites.Values)
            {
                e.Update(t); // Entity Update
            }

            foreach(GameEntity g in newEntities.Values)
            {
                entites.Add(g.Id, g);
            }

            foreach (long i in deletedEntities)
            {
                entites.Remove(i);
            }

            newEntities.Clear();
            deletedEntities.Clear();
        }
        
        public void AddEntity(GameEntity e)
        {
            newEntities.Add(e.Id, e);
        }

        public Vector2 GetPlayerPosition()
        {
            if (playerId > -1)
            {
                long playerPhysId = entites[playerId].PhysicsId;
                return GameSession.Instance.PhysicsManager.Get(playerPhysId).Position;
            }
            else return new Vector2(0, 0);
        }

        public void Delete(long id)
        {
            if (id > -1) deletedEntities.Add(id);
        }
    }
}
