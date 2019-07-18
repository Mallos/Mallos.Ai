using GoRogueSample.Screens;

namespace GoRogueSample
{
    class Program
    {
        private const int StartingWidth = 100;
        private const int StartingHeight = 40;

        public static MapScreen MapScreen { get; set; }

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(StartingWidth, StartingHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            MapScreen = new MapScreen(StartingWidth, StartingHeight);
            SadConsole.Global.CurrentScreen = MapScreen;
        }
    }
}