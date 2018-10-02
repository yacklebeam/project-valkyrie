﻿using System;
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
        public SquareZone() : base()
        {
            Type = EntityType.ZONE;
            Texture = GameSession.Instance.AssetManager.getTexture("hero");
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