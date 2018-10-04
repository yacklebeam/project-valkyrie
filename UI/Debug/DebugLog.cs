using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectValkyrie.Managers;
using System.Collections.Generic;

namespace ProjectValkyrie.UI.Debug
{
    class DebugLog
    {
        private class DebugMessage
        {
            private string message;
            private float ttl;

            public string Message { get => message; set => message = value; }
            public float Ttl { get => ttl; set => ttl = value; }
        }


        private readonly List<DebugMessage> debugMessages;
        private Vector2 position;

        public DebugLog()
        {
            position = new Vector2(10.0f, 50.0f);
            debugMessages = new List<DebugMessage>();
        }

        public void AddMessage(string msg)
        {
            DebugMessage m = new DebugMessage();
            m.Message = msg;
            m.Ttl = 3.0f;
            debugMessages.Add(m);
            if (debugMessages.Count > 30) debugMessages.RemoveAt(0);
        }

        public void Update(GameTime t)
        {
            foreach(DebugMessage d in debugMessages)
            {
                d.Ttl -= (float)t.ElapsedGameTime.TotalSeconds;
            }

            if (debugMessages.Count > 0 && debugMessages[0].Ttl <= 0.0f)
            {
                debugMessages.RemoveAt(0);
            }
        }

        public void Render(SpriteBatch sb)
        {
            SpriteFont font = GameSession.Instance.AssetManager.getFont("debug-font");

            Vector2 pos = Position;
            foreach(DebugMessage msg in debugMessages)
            {
                sb.DrawString(font, msg.Message, pos, Color.White);
                pos += new Vector2(0, 16.0f);
            }
        }

        public Vector2 Position { get => position; set => position = value; }
    }
}
