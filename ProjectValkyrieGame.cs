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

        EntityManager e;
        AssetManager a;
        PhysicsManager p;
        RenderManager r;

        HUD hud;

        int globalID = 0;

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
            e = new EntityManager();
            p = new PhysicsManager(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Vector2(64.0f, 36.0f));
            a = new AssetManager();
            r = new RenderManager();
            hud = new HUD();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            a.loadImageAsset("hero", "images/Hero", Content);
            a.loadImageAsset("goblin", "images/Goblin", Content);
            a.loadImageAsset("hitbox", "images/Hitbox", Content);
            a.loadImageAsset("active-firetrap", "images/FireTrapActive", Content);
            a.loadImageAsset("firetrap", "images/FireTrap", Content);
            a.loadFontAsset("debug-font", "fonts/DebugFont", Content);

            DummyLoadLevel();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            p.Update(gameTime, e);
            e.Update(gameTime);
            hud.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSlateGray);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
            r.Render(spriteBatch, p, a); // Renders textures based on physical location and state value
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
            Goblin gob = new Goblin(globalID++);
            PhysicsComponent gobPc = new PhysicsComponent(gob.Id);
            gobPc.Type = PhysicsComponent.PhysicsType.COLLIDE;
            gobPc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 0.5f, 0.5f);
            gobPc.Position = pos;
            p.Add(gobPc);
            RenderComponent gobRc = new RenderComponent(gob.Id);
            gobRc.TextureName = "goblin";
            gobRc.Offset = new Vector2(-10.0f, -10.0f);
            r.Add(gobRc);
            e.Add(gob);
        }

        private void DummyLoadHero(Vector2 pos)
        {
            // Load the Hero
            Hero hero = new Hero(globalID++);

            PhysicsComponent pc = new PhysicsComponent(hero.Id);
            pc.Type = PhysicsComponent.PhysicsType.NONE;
            pc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1, 1);
            pc.Position = pos;
            p.Add(pc);

            RenderComponent rc = new RenderComponent(hero.Id);
            rc.TextureName = "hero";
            rc.Offset = new Vector2(-10.0f, -10.0f);
            r.Add(rc);

            e.Add(hero);
            e.PlayerId = hero.Id;
        }

        private void DummyLoadFireTrap(Vector2 pos, bool active)
        {
            FireTrap szone = new FireTrap(globalID++, active);

            PhysicsComponent spc = new PhysicsComponent(szone.Id);
            spc.Type = PhysicsComponent.PhysicsType.INTERSECT;
            spc.Hitbox = MathUtils.GetRectangleHitbox(new Vector2(0, 1), 1.5f, 1.5f);
            spc.Position = pos;
            p.Add(spc);

            RenderComponent src = new RenderComponent(szone.Id);
            src.TextureName = (active)?"active-firetrap": "firetrap";
            src.Offset = new Vector2(-20.0f, -20.0f);
            r.Add(src);

            e.Add(szone);
        }
    }
}
