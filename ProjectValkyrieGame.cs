using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectValkyrie.Components;
using ProjectValkyrie.Entities;
using ProjectValkyrie.Managers;

namespace ProjectValkyrie
{
    public class ProjectValkyrieGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameSession _gameSession = GameSession.Instance;

        public ProjectValkyrieGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _gameSession.EntityManager = new EntityManager();
            _gameSession.PhysicsManager = new PhysicsManager(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Vector2(640.0f, 360.0f));
            _gameSession.AssetManager = new AssetManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameSession.AssetManager.loadImageAsset("hero", "images/hero", Content);
            DummyLoadLevel();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _gameSession.PhysicsManager.Update(gameTime); // Updates physical position of objects in game world
            _gameSession.EntityManager.Update(gameTime); // Updates game states based on changes to physical world
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            _gameSession.EntityManager.Render(spriteBatch);
            //_gameSession.RenderManager.Render(spriteBatch); // Renders textures based on physical location and state value
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            Hero hero = new Hero();
            PhysicsComponent pc = new PhysicsComponent(hero.Id);
            pc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            pc.Hitbox = Math.MathUtils.GetRectangleHitbox(new Vector2(0, 1), 100, 100);
            pc.Position = new Vector2(100.0f, 100.0f);
            hero.PhysicsId = _gameSession.PhysicsManager.Add(pc);
            _gameSession.EntityManager.AddEntity(hero);

            SquareZone szone = new SquareZone();
            PhysicsComponent spc = new PhysicsComponent(szone.Id);
            spc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            spc.Hitbox = Math.MathUtils.GetRectangleHitbox(new Vector2(0, 1), 100, 100);
            spc.Position = new Vector2(110.0f, 110.0f);
            szone.PhysicsId = _gameSession.PhysicsManager.Add(spc);
            _gameSession.EntityManager.AddEntity(szone);
        }
    }
}
