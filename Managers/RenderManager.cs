using System.Collections.Generic;
using ProjectValkyrie.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectValkyrie.Managers
{
    class RenderManager
    {
        private readonly Dictionary<long, RenderComponent> renders;
        private int currentId = 0;

        public RenderManager()
        {
            renders = new Dictionary<long, RenderComponent>();
        }

        public RenderComponent Get(long id)
        {
            return renders[id];
        }

        public long Add(RenderComponent r)
        {
            currentId++;
            long id = currentId;
            r.Id = id;
            renders.Add(id, r);
            return id;
        }

        public void Render(SpriteBatch s)
        {
            foreach(RenderComponent r in renders.Values)
            {
                r.Render(s);
            }
        }

        public void Delete(long id)
        {
            if (id > -1) renders.Remove(id);
        }
    }
}
