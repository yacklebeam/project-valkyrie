using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ValhallaEngine.Managers;

namespace ProjectValkyrie.UI
{
    class HUD
    {
        private HealthBar healthBar;

        public HUD()
        {
            healthBar = new HealthBar();
            healthBar.MaxHealth = 100;
            healthBar.CurrentHealth = 100;
        }

        public void Update()
        {
            healthBar.Update();
        }

        public void Render(SpriteBatch sb)
        {
            healthBar.Render(sb);
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
        }

        public void Render(SpriteBatch sb)
        {
            SpriteFont font = GameSession.Instance.AssetManager.getFont("debug-font");

            sb.DrawString(font, currentHealth + " / " + maxHealth, new Vector2(10.0f, 10.0f), Color.White);
        }
    }

}
