using System;
using Microsoft.Xna.Framework;
using ValhallaEngine.Managers;
using ValhallaEngine.Entities;
using ValhallaEngine.Math;
using ValhallaEngine.Components;

namespace ProjectValkyrie.Entities
{
    class SquareZone : GameEntity
    {
        public SquareZone(Vector2 position) : base()
        {
            PhysicsComponent pc = new PhysicsComponent(Id);
            pc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            pc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 100, 100);
            pc.Position = position;

            Type = EntityType.ZONE;

            Texture = GameSession.Instance.AssetManager.getTexture("hero");
            PhysicsId = GameSession.Instance.PhysicsManager.Add(pc);
        }

        public override void OnEvent(long id)
        {
            Console.WriteLine("SquareZone OnEvent() triggered");
        }

        public override void OnUpdate(GameTime t)
        {
        }
    }
}
