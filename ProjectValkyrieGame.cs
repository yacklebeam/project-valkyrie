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

        GameSession _gs = GameSession.Instance;

        HUD hud;

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
            _gs.EntityManager = new EntityManager();
            _gs.PhysicsManager = new PhysicsManager(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Vector2(64.0f, 36.0f));
            _gs.AssetManager = new AssetManager();
            _gs.RenderManager = new RenderManager();
            hud = new HUD();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _gs.AssetManager.loadImageAsset("hero", "images/Hero", Content);
            _gs.AssetManager.loadImageAsset("goblin", "images/Goblin", Content);
            _gs.AssetManager.loadImageAsset("hitbox", "images/Hitbox", Content);
            _gs.AssetManager.loadImageAsset("active-firetrap", "images/FireTrapActive", Content);
            _gs.AssetManager.loadImageAsset("firetrap", "images/FireTrap", Content);
            _gs.AssetManager.loadFontAsset("debug-font", "fonts/DebugFont", Content);

            DummyLoadLevel();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _gs.PhysicsManager.Update(gameTime, _gs.EntityManager);
            _gs.EntityManager.Update(gameTime);
            hud.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSlateGray);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            _gs.RenderManager.Render(spriteBatch, _gs.PhysicsManager, _gs.AssetManager); // Renders textures based on physical location and state value
            hud.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            DummyLoadHero(new Vector2(25.0f, 25.0f));
        }

        private void DummyLoadGoblin(Vector2 pos)
        {
            Goblin gob = new Goblin(GameSession.NextID);
            PhysicsComponent gobPc = new PhysicsComponent(gob.Id);
            gobPc.Type = PhysicsComponent.PhysicsType.COLLIDE;
            gobPc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 0.5f, 0.5f);
            gobPc.Position = pos;
            _gs.PhysicsManager.Add(gobPc);
            RenderComponent gobRc = new RenderComponent(gob.Id);
            gobRc.TextureName = "goblin";
            gobRc.Offset = new Vector2(-10.0f, -10.0f);
            _gs.RenderManager.Add(gobRc);
            _gs.EntityManager.Add(gob);
        }

        private void DummyLoadHero(Vector2 pos)
        {
            // Load the Hero
            Hero hero = new Hero(GameSession.NextID);

            PhysicsComponent pc = new PhysicsComponent(hero.Id);
            pc.Type = PhysicsComponent.PhysicsType.NONE;
            pc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1, 1);
            pc.Position = pos;
            _gs.PhysicsManager.Add(pc);

            RenderComponent rc = new RenderComponent(hero.Id);
            rc.TextureName = "hero";
            rc.Offset = new Vector2(-10.0f, -10.0f);
            _gs.RenderManager.Add(rc);

            _gs.EntityManager.Add(hero);
            _gs.EntityManager.PlayerId = hero.Id;
        }

        private void DummyLoadFireTrap(Vector2 pos, bool active)
        {
            FireTrap szone = new FireTrap(GameSession.NextID, active);

            PhysicsComponent spc = new PhysicsComponent(szone.Id);
            spc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            spc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1.5f, 1.5f);
            spc.Position = pos;
            _gs.PhysicsManager.Add(spc);

            RenderComponent src = new RenderComponent(szone.Id);
            src.TextureName = (active)?"active-firetrap": "firetrap";
            src.Offset = new Vector2(-20.0f, -20.0f);
            _gs.RenderManager.Add(src);

            _gs.EntityManager.Add(szone);
        }
    }
}
