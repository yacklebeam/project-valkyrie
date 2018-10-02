namespace ProjectValkyrie.Managers
{
    public sealed class GameSession
    {
        private static readonly GameSession instance = new GameSession();

        private PhysicsManager  physics = null;
        private EntityManager   entity = null;
        private RenderManager   render = null;
        private AssetManager    asset = null;

        static GameSession() { }
        private GameSession()
        {
        }

        public static GameSession Instance
        {
            get => instance;
        }

        internal PhysicsManager PhysicsManager { get => physics; set => physics = value; }
        internal EntityManager EntityManager { get => entity; set => entity = value; }
        internal RenderManager RenderManager { get => render; set => render = value; }
        internal AssetManager AssetManager { get => asset; set => asset = value; }
    }
}
