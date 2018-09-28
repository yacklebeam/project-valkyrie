using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using System;

namespace ProjectValkyrie.Entities
{
    class Ogre : Base.GameEntity
    {
        public Ogre() : base()
        {
            TriggerType = -1;// No triggers on this entity
            HasPhysics = false;
        }

        public override void OnEvent(GameEntity g)
        { }

        public override void OnUpdate(GameTime t)
        {
            // Perform in game logic here
            Console.WriteLine("Ogre updated: " + (float)(t.ElapsedGameTime.Ticks / 10000.0f));
        }
    }
}
