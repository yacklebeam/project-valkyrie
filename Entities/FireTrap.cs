using Microsoft.Xna.Framework;
using ProjectValkyrie.Entities.Base;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Entities
{
    class FireTrap : GameEntity
    {
        private bool active;
        private float activeTime;

        public FireTrap() : base()
        {
            Type = EntityType.ZONE;
            activeTime = 10.0f;
            active = false;
        }

        public FireTrap(bool a): base()
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
                    SetTexture("active-firetrap");
                    activeTime += 5.0f;
                }
                else
                {
                    SetTexture("firetrap");
                    activeTime += 15.0f;
                }
            }
        }
    }
}
