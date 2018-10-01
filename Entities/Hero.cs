using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectValkyrie.Entities.Base;

namespace ProjectValkyrie.Entities
{
    class Hero : Base.GameEntity
    {
        private ProjectValkyrie.Items.Base.GameItem mainWeapon = null;

        public Hero(Managers.AssetManager a)
        {
            HasRenderable = true;
            Physics = new Components.PhysicsComponent();

            Health = 100;
            Speed = 5.0f;

            Texture = a.getTexture("hero");

            mainWeapon = new Items.SquireSword();
        }

        public override void OnEvent(GameEntity ge)
        {
            
        }

        public override void OnUpdate(GameTime t)
        {
            Vector2 leftStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1.0f);
            Physics.Velocity = Speed * leftStickNormalized;

            if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                if (mainWeapon != null) mainWeapon.OnUsePrimary();
            }
        }
    }
}
