using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectValkyrie.Entities.Base;
using System;

namespace ProjectValkyrie.Entities
{
    class Ogre : Base.GameEntity
    {
        public Ogre(Managers.AssetManager a) : base()
        {
                HasRenderable = true;
                Physics = new Components.PhysicsComponent();

                Health = 100;
                Speed = 0.5f;

                Texture = a.getTexture("hero");
            }

        public override void OnEvent(GameEntity g)
        { }

        public override void OnUpdate(GameTime t)
        {
            Vector2 rightStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y * -1.0f);
            Physics.Velocity = Speed * rightStickNormalized;
        }
    }
}
