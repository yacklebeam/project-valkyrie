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

        public ProjectValkyrieGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _em = new EntityManager();
            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load all of your content.
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            DummyLoadLevel();
            // TODO: use this.Content to load your game content here
        }

        /// UnloadContent will be called once per game and is the place to unload game-specific content.
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _em.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            Entities.Ogre ogre = new Entities.Ogre();
            _em.AddEntity(_em.GetNextID(), ogre);
        }
    }
}
