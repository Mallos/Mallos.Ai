using GoRogue;
using GoRogue.GameFramework;
using GoRogueSample.Dungeon;
using GoRogueSample.Monsters;
using SadConsole;

namespace GoRogueSample.Screens
{
    class MapScreen : ContainerConsole
    {
        public ExampleMap Map { get; }
        public ScrollingConsole MapRenderer { get; }

        public StatsPanel StatsPanel { get; }
        public LogsPanel LogsPanel { get; }

        public MapScreen(int viewportWidth, int viewportHeight)
        {
            // TODO: Improve this, I would like to not be limited by map size.
            const int panelStatsWidth = 20;
            const int panelLogsHeight = 10;

            int mapWidth = viewportWidth - panelStatsWidth;
            int mapHeight = viewportHeight - panelLogsHeight;


            // Add stats panel
            StatsPanel = new StatsPanel(panelStatsWidth, viewportHeight);
            StatsPanel.Position = new Microsoft.Xna.Framework.Point(mapWidth, 0); 
            Children.Add(StatsPanel);

            // Add logs panel
            LogsPanel = new LogsPanel(mapWidth, panelLogsHeight);
            LogsPanel.Position = new Microsoft.Xna.Framework.Point(0, mapHeight);
            Children.Add(LogsPanel);

            Map = DungeonBuilder.GenerateDungeon(mapWidth, mapHeight);

            // Get a console that's set up to render the map, 
            // and add it as a child of this container so it renders
            MapRenderer = Map.CreateRenderer(0, 0, mapWidth, mapHeight);
            Children.Add(MapRenderer);
            
            // Set player to receive input, since in this example the player handles movement
            Map.ControlledGameObject.IsFocused = true; 

            // Set up to recalculate FOV and set camera position appropriately when the player moves
            Map.ControlledGameObject.Moved += Player_Moved;

            // Calculate initial FOV and center camera
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
        }

        private void Player_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
        {
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);

            // TODO: Start using GameFrameManager instead
            TickWorld();
        }

        private void TickWorld()
        {
            foreach (var entity in Map.Entities)
            {
                if (entity.Item is Monster monster)
                {
                    monster.WorldTick();
                }
            }
        }
    }
}
