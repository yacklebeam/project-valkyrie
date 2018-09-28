using Microsoft.Xna.Framework;


namespace ProjectValkyrie.Entities.Base
{
    abstract class GameEntity
    {
        private long id;
        private int triggerType;

        public long Id { get => id; set => id = value; }
        public int TriggerType { get => triggerType; set => triggerType = value; }

        public abstract void OnUpdate(GameTime t);
        public abstract void OnTrigger(GameEntity ge);

        public GameEntity()
        { }

        public void Update(GameTime t)
        {// For now, all entities update every update call
            OnUpdate(t);
        }
    }
}
