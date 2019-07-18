using GoRogue;
using GoRogue.GameFramework;
using GoRogueSample.Monsters;
using SadConsole;

namespace GoRogueSample
{
    class MapScreen : ContainerConsole
    {
        public ExampleMap Map { get; }
        public ScrollingConsole MapRenderer { get; }

        //Generate a map and display it.  Could just as easily pass it into
        public MapScreen(int mapWidth, int mapHeight, int viewportWidth, int viewportHeight)
        {
            Map = DungeonBuilder.GenerateDungeon(mapWidth, mapHeight);

            // Get a console that's set up to render the map, and add it as a child of this container so it renders
            MapRenderer = Map.CreateRenderer(0, 0, viewportWidth, viewportHeight);
            Children.Add(MapRenderer);
            Map.ControlledGameObject.IsFocused = true; // Set player to receive input, since in this example the player handles movement

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
