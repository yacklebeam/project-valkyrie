using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ValhallaEngine.Components;
using ProjectValkyrie.Entities;
using ValhallaEngine.Math;
using ValhallaEngine.Managers;
using ProjectValkyrie.UI;

namespace ProjectValkyrie
{
    public class ProjectValkyrieGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        HUD hud;

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
            _gameSession.RenderManager = new RenderManager();
            hud = new HUD();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameSession.AssetManager.loadImageAsset("hero", "images/Hero", Content);
            _gameSession.AssetManager.loadImageAsset("goblin", "images/Goblin", Content);
            _gameSession.AssetManager.loadImageAsset("hitbox", "images/Hitbox", Content);
            _gameSession.AssetManager.loadImageAsset("active-firetrap", "images/FireTrapActive", Content);
            _gameSession.AssetManager.loadImageAsset("firetrap", "images/FireTrap", Content);
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
            _gameSession.PhysicsManager.Update(gameTime);
            _gameSession.EntityManager.Update(gameTime);
            hud.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSlateGray);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            //_gameSession.PhysicsManager.Render(spriteBatch);
            _gameSession.RenderManager.Render(spriteBatch); // Renders textures based on physical location and state value
            hud.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            DummyLoadGoblin(new Vector2(10.0f, 10.0f));
            DummyLoadGoblin(new Vector2(15.0f, 10.0f));
            DummyLoadGoblin(new Vector2(50.0f, 30.0f));
            DummyLoadGoblin(new Vector2(35.0f, 20.0f));

            DummyLoadHero(new Vector2(25.0f, 25.0f));
        }

        private void DummyLoadGoblin(Vector2 pos)
        {
            Goblin gob = new Goblin();
            PhysicsComponent gobPc = new PhysicsComponent(gob.Id);
            gobPc.Type = PhysicsComponent.PhysicsType.COLLIDE;
            gobPc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 0.5f, 0.5f);
            gobPc.Position = pos;
            gob.PhysicsId = _gameSession.PhysicsManager.Add(gobPc);
            RenderComponent gobRc = new RenderComponent(gob.PhysicsId);
            gobRc.TextureName = "goblin";
            gobRc.Offset = new Vector2(-10.0f, -10.0f);
            gob.RenderId = _gameSession.RenderManager.Add(gobRc);
            _gameSession.EntityManager.AddEntity(gob);
        }

        private void DummyLoadHero(Vector2 pos)
        {
            // Load the Hero
            Hero hero = new Hero();

            PhysicsComponent pc = new PhysicsComponent(hero.Id);
            pc.Type = PhysicsComponent.PhysicsType.NONE;
            pc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1, 1);
            pc.Position = pos;
            hero.PhysicsId = _gameSession.PhysicsManager.Add(pc);

            RenderComponent rc = new RenderComponent(hero.PhysicsId);
            rc.TextureName = "hero";
            rc.Offset = new Vector2(-10.0f, -10.0f);
            hero.RenderId = _gameSession.RenderManager.Add(rc);

            _gameSession.EntityManager.AddEntity(hero);
            _gameSession.EntityManager.PlayerId = hero.Id;
        }

        private void DummyLoadFireTrap(Vector2 pos, bool active)
        {
            FireTrap szone = new FireTrap(active);

            PhysicsComponent spc = new PhysicsComponent(szone.Id);
            spc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            spc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1.5f, 1.5f);
            spc.Position = pos;
            szone.PhysicsId = _gameSession.PhysicsManager.Add(spc);

            RenderComponent src = new RenderComponent(szone.PhysicsId);
            src.TextureName = (active)?"active-firetrap": "firetrap";
            src.Offset = new Vector2(-20.0f, -20.0f);
            szone.RenderId = _gameSession.RenderManager.Add(src);

            _gameSession.EntityManager.AddEntity(szone);
        }
    }
}
