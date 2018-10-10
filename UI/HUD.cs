using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ValhallaEngine.Manager;

namespace ProjectValkyrie.UI
{
    class HUD
    {
        private HealthBar healthBar;
        private EntityCount entityCount;

        public HUD()
        {
            healthBar = new HealthBar();
            healthBar.MaxHealth = 0;
            healthBar.CurrentHealth = 0;

            entityCount = new EntityCount();
            entityCount.CurrentCount = GameSession.Instance.EntityManager.Count;
        }

        public void Update()
        {
            healthBar.Update();
            entityCount.Update();
        }

        public void Render(SpriteBatch sb)
        {
            healthBar.Render(sb);
            entityCount.Render(sb);
        }
    }

    internal class HealthBar
    {
        private int maxHealth;
        private int currentHealth;

        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

        public void Update()
        {
            currentHealth = GameSession.Instance.EntityManager.Get(GameSession.Instance.EntityManager.PlayerId).Health;
            maxHealth = GameSession.Instance.EntityManager.Get(GameSession.Instance.EntityManager.PlayerId).MaxHealth;
        }

        public void Render(SpriteBatch sb)
        {
            SpriteFont font = GameSession.Instance.AssetManager.getFont("debug-font");

            sb.DrawString(font, currentHealth + " / " + maxHealth, new Vector2(10.0f, 10.0f), Color.White);
        }
    }

    internal class EntityCount
    {
        private int currentCount;

        public int CurrentCount { get => currentCount; set => currentCount = value; }

        public void Update()
        {
            currentCount = GameSession.Instance.EntityManager.Count;
        }

        public void Render(SpriteBatch sb)
        {
            SpriteFont font = GameSession.Instance.AssetManager.getFont("debug-font");

            sb.DrawString(font, "Entity Count: " + currentCount, new Vector2(10.0f, 30.0f), Color.White);
        }
    }

}
