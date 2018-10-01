using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;

namespace ProjectValkyrie.Items
{
    class SquireSword : Base.GameItem
    {
        public override void OnUsePrimary(GameEntity e)
        {
            // Generate the hitbox entity for this item
            SquireSwordPrimaryAttack attack = new SquireSwordPrimaryAttack();
        }

        public override void OnUseSecondary(GameEntity e)
        {
        }
    }

    // The sword primary attack associated with the sword
    class SquireSwordPrimaryAttack : GameEntity
    {
        private readonly int damage = 5;

        public override void OnEvent(GameEntity ge)
        {
            ge.Health -= damage;
        }

        public override void OnUpdate(GameTime t)
        {}
    }

    // The sword secondary attack associated with the sword
    class SquireSwordSecondaryAttack : GameEntity
    {
        private readonly int damage = 5;

        public override void OnEvent(GameEntity ge)
        {
            ge.Health -= damage;
        }

        public override void OnUpdate(GameTime t)
        {
        }
    }
}
