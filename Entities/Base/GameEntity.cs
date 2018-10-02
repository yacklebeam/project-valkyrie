using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        //private PhysicsComponent physics = null;
        private long physicsId = -1;
        private bool hasRenderable = false;
        private EntityType type = EntityType.NONE;

        private Texture2D texture = null;

        // State Values
        private int health;
        private float speed;

        private int maxHealth;
        
        public long Id { get => id; set => id = value; }
        public int TriggerType { get => triggerType; set => triggerType = value; }
        public long TargetId { get => targetId; set => targetId = value; }
        public bool HasRenderable { get => hasRenderable; set => hasRenderable = value; }
        public EntityType Type { get => type; set => type = value; }
        public int Health { get => health; set => health = value; }
        public float Speed { get => speed; set => speed = value; }
        public Texture2D Texture { get => texture; set => texture = value; }
        public long PhysicsId { get => physicsId; set => physicsId = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }

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

        public void Render(SpriteBatch sb)
        {
            if (texture != null) sb.Draw(texture, GameSession.Instance.PhysicsManager.ConvertToScreenCoordinates(GameSession.Instance.PhysicsManager.Get(physicsId).Position), Color.White);
        }

        public void AddHealth(int delta)
        {
            health += delta;
            if (health > maxHealth) health = maxHealth;
        }

        public void SubtractHealth(int delta)
        {
            health -= delta;
            if (health < 0) health = 0;
        }
    }
}
