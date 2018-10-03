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
        private Vector2 offset;

        public RenderComponent(long id)
        {
            physicsId = id;
            offset = new Vector2(0.0f, 0.0f);
        }

        public string TextureName { get => textureName; set => textureName = value; }
        public Vector2 Offset { get => offset; set => offset = value; }

        public void Render(SpriteBatch sb)
        {
            if (textureName != "") sb.Draw(GameSession.Instance.AssetManager.getTexture(textureName), GameSession.Instance.PhysicsManager.ConvertToScreenCoordinates(GameSession.Instance.PhysicsManager.Get(physicsId).Position) - Offset, Color.White);
        }
    }
}
