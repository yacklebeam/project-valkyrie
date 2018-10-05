using Microsoft.Xna.Framework;
using ValhallaEngine.Entities;

namespace ProjectValkyrie.Entities
{
    class DarkWizard : GameEntity
    {
        private float teleportcooldown;
        public DarkWizard(long id) : base(id)
        {
            teleportcooldown = 5.0f;
            


        }

        public override void OnEvent(long id)
        {
           
        }

        public override void OnUpdate(GameTime t)
        {
            teleportcooldown -= (float)t.ElapsedGameTime.TotalSeconds;
            if (teleportcooldown <= 0)
            {
                System.Random rand = new System.Random();
                float x = rand.Next(4, 60);
                float y = rand.Next(4, 32);
                GameSession.Instance.PhysicsManager.Get(Id).Position = new Vector2(x, y);
                teleportcooldown = 5.0f;
            }
        }
    }
}
