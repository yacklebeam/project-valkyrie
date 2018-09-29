using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie
{
    public class ProjectValkyrieGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EntityManager _em;
        AssetManager _am;

        public ProjectValkyrieGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _em = new EntityManager();
            _am = new AssetManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _am.loadImageAsset("hero", "images/hero", Content);
            DummyLoadLevel();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _em.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            _em.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            Entities.Hero hero = new Entities.Hero(_am);
            hero.Physics.Postion = new Vector2(100.0f, 100.0f);
            long playerId = _em.AddEntity(hero);

            Entities.Ogre ogre = new Entities.Ogre(_am);
            ogre.Physics.Postion = new Vector2(400.0f, 400.0f);
            long newId = _em.AddEntity(ogre);
        }
    }
}
