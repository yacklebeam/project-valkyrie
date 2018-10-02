namespace ProjectValkyrie.Items.Base
{
    abstract class GameItem
    {
        public abstract void OnUsePrimary(Entities.Base.GameEntity e);
        public abstract void OnUseSecondary(Entities.Base.GameEntity e);

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
