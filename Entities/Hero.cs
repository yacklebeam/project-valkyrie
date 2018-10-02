﻿using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using Microsoft.Xna.Framework.Input;
using System;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities
{
    class Hero : Base.GameEntity
    {
        private Items.Base.GameItem primaryWeapon = null;

        public Hero(Vector2 position) : base()
        {
            HasRenderable = true;
            Components.PhysicsComponent pc = new Components.PhysicsComponent(Id);
            pc.Type = Components.PhysicsComponent.PhysicsType.INTERSECT;
            pc.Hitbox = Math.MathUtils.GetRectangleHitbox(new Vector2(0, 1), 100, 100);
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
