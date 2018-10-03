using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using Microsoft.Xna.Framework.Input;
using System;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities
{
    class Hero : Base.GameEntity
    {
        private Items.Base.GameItem primaryWeapon = null;
        private int counter = 0;

        public Hero() : base()
        {
            HasRenderable = true;
            primaryWeapon = new Items.SquireSword();

            MaxHealth = 100;
            Health = 100;
            Speed = 10.0f;

            Type = EntityType.PLAYER;

            // Texture should be switched to the RenderManager or AssetManager
            Texture = GameSession.Instance.AssetManager.getTexture("hero");
        }

        public override void OnEvent(long id)
        {
            GameSession.Instance.DebugLog.AddMessage("Hero OnEvent() triggered " + counter.ToString());
            counter++;
        }

        public override void OnUpdate(GameTime t)
        {
            Vector2 leftStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1.0f);
            GameSession.Instance.PhysicsManager.Get(PhysicsId).Velocity = Speed * leftStickNormalized;

            if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                PrimaryAttack();
            }
        }

        private void PrimaryAttack()
        {
            if (primaryWeapon != null) primaryWeapon.OnUsePrimary(this);
        }
    }
}
