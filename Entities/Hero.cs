using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using ValhallaEngine.Managers;
using ValhallaEngine.Components;
using ValhallaEngine.Entities;
using ValhallaEngine.Math;

namespace ProjectValkyrie.Entities
{
    class Hero : GameEntity
    {
        private Items.Base.GameItem primaryWeapon = null;

        public Hero(Vector2 position) : base()
        {
            HasRenderable = true;
            PhysicsComponent pc = new PhysicsComponent(Id);
            pc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            pc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 100, 100);
            pc.Position = position;
            primaryWeapon = new Items.SquireSword();

            MaxHealth = 100;
            Health = 100;
            Speed = 5.0f;

            Type = EntityType.PLAYER;

            // Texture should be switched to the RenderManager or AssetManager
            Texture = GameSession.Instance.AssetManager.getTexture("hero");
            PhysicsId = GameSession.Instance.PhysicsManager.Add(pc);
        }

        public override void OnEvent(long id)
        {
            Console.WriteLine("Hero OnEvent() triggered");
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
