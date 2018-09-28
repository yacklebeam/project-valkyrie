using Microsoft.Xna.Framework;


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
        private int triggerType;
        private long targetId = -1;
        private bool hasPhysics = false;
        private bool hasRenderable = false;
        private EntityType type;

        public long Id { get => id; set => id = value; }
        public int TriggerType { get => triggerType; set => triggerType = value; }
        public long TargetId { get => targetId; set => targetId = value; }
        public bool HasPhysics { get => hasPhysics; set => hasPhysics = value; }
        public bool HasRenderable { get => hasRenderable; set => hasRenderable = value; }
        internal EntityType Type { get => type; set => type = value; }

        public abstract void OnUpdate(GameTime t); // Called every update cycle for this entity by Update() call
        public abstract void OnEvent(GameEntity ge); // Triggers are based on physics collide or intersection events, independent of this entities updates

        public GameEntity()
        {// Sets the Entity Defaults -- subclasses should override these values if needed
            TriggerType = -1;
            HasPhysics = false;
            hasRenderable = false;
            Type = EntityType.NONE;
        }

        public void Update(GameTime t)
        {// For now, all entities update every update call
            OnUpdate(t);
        }
    }
}
