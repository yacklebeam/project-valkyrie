using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities
{
    class SquareZone : Base.GameEntity
    {
        public SquareZone(Vector2 position) : base()
        {
            Components.PhysicsComponent pc = new Components.PhysicsComponent(Id);
            pc.Type = Components.PhysicsComponent.PhysicsType.INTERSECT;
            pc.Hitbox = Math.MathUtils.GetRectangleHitbox(new Vector2(0, 1), 100, 100);
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
