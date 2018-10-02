using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using Microsoft.Xna.Framework.Input;
using System;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities
{
    class Hero : Base.GameEntity
    {
        public Hero() : base()
        {
            HasRenderable = true;
            Components.PhysicsComponent pc = new Components.PhysicsComponent();

            Health = 100;
            Speed = 5.0f;

            // Texture should be switched to the RenderManager or AssetManager
            Texture = GameSession.Instance.AssetManager.getTexture("hero");
            PhysicsId = GameSession.Instance.PhysicsManager.Add(pc);
        }

        public override void OnEvent(GameEntity ge)
        {
            
        }

        public override void OnUpdate(GameTime t)
        {
            Vector2 leftStickNormalized = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1.0f);
            GameSession.Instance.PhysicsManager.Get(PhysicsId).Velocity = Speed * leftStickNormalized;
        }
    }
}
