using Microsoft.Xna.Framework;
using ValhallaEngine.Entities;

namespace ProjectValkyrie.Entities
{
    class FireTrap : GameEntity
    {
        private bool active;
        private float activeTime;

        public FireTrap(long id) : base(id)
        {
            Type = EntityType.ZONE;
            activeTime = 10.0f;
            active = false;
        }

        public FireTrap(long id, bool a): base(id)
        {
            Type = EntityType.ZONE;
            activeTime = (a)?5.0f:10.0f;
            active = a;
        }

        public override void OnEvent(long id)
        {
            GameEntity ge = GameSession.Instance.EntityManager.Get(id);
            if (active && ge.Type == EntityType.PLAYER)
            {
                ge.SubtractHealth(5);
            }
        }

        public override void OnUpdate(GameTime t)
        {
            activeTime -= (float)t.ElapsedGameTime.TotalSeconds;
            if(activeTime < 0.0f)
            {
                active = !active;
                if (active)
                {
                    GameSession.Instance.RenderManager.Get(Id).TextureName = "firetrap-active";
                    activeTime += 5.0f;
                }
                else
                {
                    GameSession.Instance.RenderManager.Get(Id).TextureName = "firetrap";
                    activeTime += 15.0f;
                }
            }
        }
    }
}
