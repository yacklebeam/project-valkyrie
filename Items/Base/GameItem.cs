using ValhallaEngine.Entities;

namespace ProjectValkyrie.Items.Base
{
    abstract class GameItem
    {
        public abstract void OnUsePrimary(GameEntity e);
        public abstract void OnUseSecondary(GameEntity e);

        // Allow classes to call these without entities
        public void OnUsePrimary()
        {
            OnUsePrimary(null);
        }

        public void OnUseSecondary()
        {
            OnUseSecondary(null);
        }
    }
}
