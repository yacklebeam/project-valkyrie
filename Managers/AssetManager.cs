using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectValkyrie.Managers
{
    class AssetManager
    {
        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;
        private Dictionary<string, SoundEffect> sounds;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            sounds = new Dictionary<string, SoundEffect>();
        }

        public void loadImageAsset(string imageId, string imagePath, ContentManager content)
        {
            if (!textures.ContainsKey(imageId))
            {
                Texture2D newTexture = content.Load<Texture2D>(imagePath);
                textures.Add(imageId, newTexture);
            }
        }

        public void loadFontAsset(string fontId, string fontPath, ContentManager content)
        {
            if (!fonts.ContainsKey(fontId))
            {
                SpriteFont newFont = content.Load<SpriteFont>(fontPath);
                fonts.Add(fontId, newFont);
            }
        }

        public void loadSoundAsset(string soundId, string soundPath, ContentManager content)
        {
            if (!sounds.ContainsKey(soundId))
            {
                SoundEffect newSound = content.Load<SoundEffect>(soundPath);
                sounds.Add(soundId, newSound);
            }
        }

        public Texture2D getTexture(string textureId)
        {
            return textures[textureId];
        }

        public SpriteFont getFont(string fontId)
        {
            return fonts[fontId];
        }

        public SoundEffect getSound(string soundId)
        {
            return sounds[soundId];
        }
    }
}
