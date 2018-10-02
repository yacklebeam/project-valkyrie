using System.Collections.Generic;
using ProjectValkyrie.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectValkyrie.Managers
{
    class RenderManager
    {
        private readonly Dictionary<long, RenderComponent> renders;

        public RenderManager()
        {
            renders = new Dictionary<long, RenderComponent>();
        }

        public void Render(SpriteBatch s)
        {
            foreach(RenderComponent r in renders.Values)
            {
                r.Render(s);
            }
        }
    }
}
