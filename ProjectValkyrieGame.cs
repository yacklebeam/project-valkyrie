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
            _gameSession.PhysicsManager = new PhysicsManager(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Vector2(64.0f, 36.0f));
            _gameSession.AssetManager = new AssetManager();
            _gameSession.DebugLog = new UI.Debug.DebugLog();
            _gameSession.RenderManager = new RenderManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameSession.AssetManager.loadImageAsset("hero", "images/Hero", Content);
            _gameSession.AssetManager.loadImageAsset("hitbox", "images/Hitbox", Content);
            _gameSession.AssetManager.loadFontAsset("debug-font", "fonts/DebugFont", Content);

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
            _gameSession.DebugLog.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            _gameSession.PhysicsManager.Render(spriteBatch);
            _gameSession.RenderManager.Render(spriteBatch); // Renders textures based on physical location and state value
            _gameSession.DebugLog.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            // Load the Hero
            Hero hero = new Hero();
            PhysicsComponent pc = new PhysicsComponent(hero.Id);
            pc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            pc.Hitbox = Math.MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1, 1);
            pc.Position = new Vector2(1.0f, 1.0f);
            hero.PhysicsId = _gameSession.PhysicsManager.Add(pc);
            RenderComponent rc = new RenderComponent(hero.PhysicsId);
            rc.TextureName = "hero";
            rc.Offset = new Vector2(-10.0f, -10.0f);
            hero.RenderId = _gameSession.RenderManager.Add(rc);
            _gameSession.EntityManager.AddEntity(hero);

            // Load a Square Zone
            SquareZone szone = new SquareZone();
            PhysicsComponent spc = new PhysicsComponent(szone.Id);
            spc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            spc.Hitbox = Math.MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1, 1);
            spc.Position = new Vector2(20.0f, 20.0f);
            szone.PhysicsId = _gameSession.PhysicsManager.Add(spc);
            RenderComponent src = new RenderComponent(szone.PhysicsId);
            src.TextureName = "hero";
            src.Offset = new Vector2(-10.0f, -10.0f);
            szone.RenderId = _gameSession.RenderManager.Add(src);
            _gameSession.EntityManager.AddEntity(szone);
        }
    }
}
