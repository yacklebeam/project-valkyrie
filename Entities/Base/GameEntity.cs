using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectValkyrie.Components;

namespace ProjectValkyrie.Entities.Base
{
    enum EntityType {
        NONE,
        PLAYER,
        ENEMY,
        PICKUP
    }

    abstract class GameEntity
    {
        private long id;
        private int triggerType = -1;
        private long targetId = -1;
        private PhysicsComponent physics = null;
        private bool hasRenderable = false;
        private EntityType type = EntityType.NONE;

        private Texture2D texture = null;

        // State Values
        private int health;
        private float speed;

        public long Id { get => id; set => id = value; }
        public int TriggerType { get => triggerType; set => triggerType = value; }
        public long TargetId { get => targetId; set => targetId = value; }
        public bool HasRenderable { get => hasRenderable; set => hasRenderable = value; }
        internal EntityType Type { get => type; set => type = value; }
        internal PhysicsComponent Physics { get => physics; set => physics = value; }
        public int Health { get => health; set => health = value; }
        public float Speed { get => speed; set => speed = value; }
        public Texture2D Texture { get => texture; set => texture = value; }

        public abstract void OnUpdate(GameTime t); // Called every update cycle for this entity by Update() call
        public abstract void OnEvent(GameEntity ge); // Triggers are based on physics collide or intersection events, independent of this entities updates

        public GameEntity()
        {}

        public void Update(GameTime t)
        {// For now, all entities update every update call
            // Also, update PhysicsComponent here
            if(physics != null) physics.Update(t);
            OnUpdate(t);
        }

        public void Render(SpriteBatch sb, Managers.PhysicsManager p)
        {
            if (texture != null) sb.Draw(texture, p.ConvertToScreenCoordinates(physics.Postion), Color.White);
        }
    }
}
