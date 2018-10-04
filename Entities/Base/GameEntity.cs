using Microsoft.Xna.Framework;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities.Base
{
    abstract class GameEntity
    {
        public enum EntityType
        {
            NONE,
            PLAYER,
            ENEMY,
            PICKUP,
            ZONE
        }

        private long id;
        private int triggerType = -1;
        private long targetId = -1;
        private long physicsId = -1;
        private long renderId = -1;
        private EntityType type = EntityType.NONE;

        // State Values
        private int health;
        private float speed;

        private int maxHealth;
        
        public long Id { get => id; set => id = value; }
        public int TriggerType { get => triggerType; set => triggerType = value; }
        public long TargetId { get => targetId; set => targetId = value; }
        public EntityType Type { get => type; set => type = value; }
        public int Health { get => health; set => health = value; }
        public float Speed { get => speed; set => speed = value; }
        public long PhysicsId { get => physicsId; set => physicsId = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public long RenderId { get => renderId; set => renderId = value; }

        public abstract void OnUpdate(GameTime t); // Called every update cycle for this entity by Update() call
        public abstract void OnEvent(long id); // Triggers are based on physics collide or intersection events, independent of this entities updates

        public GameEntity()
        {
            Id = GameSession.Instance.EntityManager.GetNextID();
        }

        public void Update(GameTime t)
        {// For now, all entities update every update call
            OnUpdate(t);
        }

        public virtual void AddHealth(int delta)
        {
            health += delta;
            if (health > maxHealth) health = maxHealth;
        }

        public virtual void SubtractHealth(int delta)
        {
            health -= delta;
            if (health < 0) health = 0;
        }

        public void SetTexture(string textureName)
        {
            if(textureName != "" && renderId != -1) GameSession.Instance.RenderManager.Get(RenderId).TextureName = textureName;
        }
    }
}
