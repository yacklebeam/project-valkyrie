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
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _em = new EntityManager();
            _em.PhysicsManager = new PhysicsManager(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Vector2(20.0f, 10.0f));
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
            hero.Physics.Postion = new Vector2(1.0f, 1.0f);
            long playerId = _em.AddEntity(hero);

            Entities.Ogre ogre = new Entities.Ogre(_am);
            ogre.Physics.Postion = new Vector2(4.0f, 4.0f);
            long newId = _em.AddEntity(ogre);
        }
    }
}
