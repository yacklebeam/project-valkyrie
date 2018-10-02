using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie.Components
{
    // Tracks the texture name, the sprite value, etc for a rendered object.
    // Does not store the Texture itself, as this would be copied data
    class RenderComponent
    {
        private string textureName;
        private long physicsId;

        public RenderComponent(long id)
        {
            physicsId = id;
        }

        public string TextureName { get => textureName; set => textureName = value; }

        public void Render(SpriteBatch sb)
        {
            if (textureName != "") sb.Draw(GameSession.Instance.AssetManager.getTexture(textureName), GameSession.Instance.PhysicsManager.ConvertToScreenCoordinates(GameSession.Instance.PhysicsManager.Get(physicsId).Position), Color.White);
        }
    }
}
