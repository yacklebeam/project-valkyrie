using ProjectValkyrie.Entities.Base;

namespace ProjectValkyrie.Items
{
    class BasicHealthPotion : Base.GameItem
    {
        private readonly int healingAmount = 10;

        public override void OnUsePrimary(GameEntity e)
        {
            if (e == null) return;
            else e.Health += healingAmount;
        }

        public override void OnUseSecondary(GameEntity e)
        {
            OnUsePrimary(e);
        }
    }
}
