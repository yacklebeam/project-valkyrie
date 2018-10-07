using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ValhallaEngine.Component;
using ProjectValkyrie.Entities;
using ValhallaEngine.Math;
using ValhallaEngine.Manager;
using ProjectValkyrie.UI;
using ValhallaEngine.Asset;

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

            GameSprite heroSprite = new GameSprite();
            heroSprite.Texture = _gs.AssetManager.getTexture("hero");
            heroSprite.Offset = new Vector2(-10, -10);
            _gs.AssetManager.AddGameSprite(0, heroSprite);

            GameSprite wizardSprite = new GameSprite();
            wizardSprite.Texture = _gs.AssetManager.getTexture("hero");
            wizardSprite.Offset = new Vector2(-10, -10);
            _gs.AssetManager.AddGameSprite(1, wizardSprite);


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
            _gs.RenderManager.Render(gameTime, spriteBatch, _gs.PhysicsManager, _gs.AssetManager); // Renders textures based on physical location and state value
            hud.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DummyLoadLevel()
        {
            // DummyLoadGoblin(new Vector2(10.0f, 10.0f));
            // DummyLoadGoblin(new Vector2(15.0f, 10.0f));
            // DummyLoadGoblin(new Vector2(50.0f, 30.0f));
            // DummyLoadGoblin(new Vector2(35.0f, 20.0f));
            DarkWizard darkwiz = new DarkWizard(GameSession.NextID);
           PhysicsComponent darkwizPc = new PhysicsComponent(darkwiz.Id);
            darkwizPc.Type = PhysicsComponent.PhysicsType.NONE;
            darkwizPc.Position = new Vector2(40.0f, 10.0f);
            darkwizPc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 0.5f, 0.5f);
            _gs.PhysicsManager.Add(darkwizPc);
            RenderComponent darkwizRc = new RenderComponent(darkwiz.Id);
            darkwizRc.SpriteID = 1;
            _gs.RenderManager.Add(darkwizRc);
            _gs.EntityManager.Add(darkwiz);

            DummyLoadHero(new Vector2(25.0f, 25.0f));
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
            rc.SpriteID = 0;
            _gs.RenderManager.Add(rc);

            _gs.EntityManager.Add(hero);
            _gs.EntityManager.PlayerId = hero.Id;
        }
    }
}
