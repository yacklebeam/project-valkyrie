using Microsoft.Xna.Framework;
using ValhallaEngine.Entities;
using Microsoft.Xna.Framework.Input;
using ValhallaEngine.Managers;

namespace ProjectValkyrie.Entities
{
    class Hero : GameEntity
    {
        private float invulnerableTime = 0.0f;

        public Hero(long id) : base(id)
        {
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
            /*if(invulnerableTime > 0.0f) invulnerableTime -= (float)t.ElapsedGameTime.TotalSeconds;
            
            Vector2 leftStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1.0f);
            GameSession.Instance.PhysicsManager.Get(PhysicsId).Velocity = Speed * leftStickNormalized;

            if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                PrimaryAttack();
            }*/
        }

        public override void SubtractHealth(int delta)
        {
            if(invulnerableTime > 0.0f)
            {
                return;
            }
            else
            {
                invulnerableTime = 1.0f;
                base.SubtractHealth(delta);
            }
        }
    }
}
