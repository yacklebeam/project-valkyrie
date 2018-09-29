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
            Speed = 0.5f;

            Texture = a.getTexture("hero");
        }

        public override void OnEvent(GameEntity ge)
        {
            
        }

        public override void OnUpdate(GameTime t)
        {
            //Console.WriteLine(String.Format("{0} {1}", GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y));
            // Read Controller Input

            Vector2 leftStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1.0f);
            Physics.Velocity = Speed * leftStickNormalized;
        }
    }
}
