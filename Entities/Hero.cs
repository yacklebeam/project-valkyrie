using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using Microsoft.Xna.Framework.Input;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities
{
    class Hero : GameEntity
    {
        private float invulnerableTime = 0.0f;
        private Items.Base.GameItem primaryWeapon = null;

        public Hero() : base()
        {
            primaryWeapon = new Items.SquireSword();

            MaxHealth = 100;
            Health = 100;
            Speed = 10.0f;

            Type = EntityType.PLAYER;
        }

        public override void OnEvent(long id)
        {
        }

        public override void OnUpdate(GameTime t)
        {
            if(invulnerableTime > 0.0f) invulnerableTime -= (float)t.ElapsedGameTime.TotalSeconds;
            
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

        public override void SubtractHealth(int delta)
        {
            if(invulnerableTime > 0.0f)
            {
                return;
            }
            else
            {
                GameSession.Instance.DebugLog.AddMessage("DANGER: Hero took damage!");

                invulnerableTime = 1.0f;
                base.SubtractHealth(delta);
            }
        }
    }
}
