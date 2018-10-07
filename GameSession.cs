using ValhallaEngine.Manager;

namespace ProjectValkyrie
{
    public sealed class GameSession
    {
        private static readonly GameSession instance = new GameSession();

        private PhysicsManager  physics = null;
        private EntityManager   entity = null;
        private RenderManager   render = null;
        private AssetManager    asset = null;

        private static int globalId = 0;

        static GameSession() { }
        private GameSession()
        {
        }

        public static GameSession Instance
        {
            get => instance;
        }

        public static long NextID
        {
            get
            {
                return globalId++;
            }
        }

        public PhysicsManager PhysicsManager { get => physics; set => physics = value; }
        public EntityManager EntityManager { get => entity; set => entity = value; }
        public RenderManager RenderManager { get => render; set => render = value; }
        public AssetManager AssetManager { get => asset; set => asset = value; }
    }
}
