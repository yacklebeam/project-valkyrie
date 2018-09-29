using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProjectValkyrie.Entities
{
    class Hero : Base.GameEntity
    {
        public Hero(Managers.AssetManager a) : base()
        {
            HasRenderable = true;
            Physics = new Components.PhysicsComponent();

            Health = 100;
            Speed = 5.0f;

            Texture = a.getTexture("hero");
        }

        public override void OnEvent(GameEntity ge)
        {
            
        }

        public override void OnUpdate(GameTime t)
        {
            Vector2 leftStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1.0f);
            Physics.Velocity = Speed * leftStickNormalized;
        }
    }
}
