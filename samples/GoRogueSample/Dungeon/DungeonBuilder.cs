using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using GoRogueSample.Monsters;
using Microsoft.Xna.Framework;
using SadConsole;

namespace GoRogueSample.Dungeon
{
    class DungeonBuilder
    {
        public static ExampleMap GenerateDungeon(int width, int height)
        {
            // Same size as screen, but we set up to center the camera on the player so expanding beyond this should work fine with no other changes.
            var map = new ExampleMap(width, height);

            // Generate map via GoRogue, and update the real map with appropriate terrain.
            var tempMap = new ArrayMap<bool>(map.Width, map.Height);
            QuickGenerators.GenerateDungeonMazeMap(tempMap, minRooms: 10, maxRooms: 20, roomMinSize: 5, roomMaxSize: 11);
            map.ApplyTerrainOverlay(tempMap, SpawnTerrain);

            Coord posToSpawn;

            // Spawn a few minotaurs
            for (int i = 0; i < 2; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true); // Get a location that is walkable
                var minotaur = new Minotaur(posToSpawn, map);
                map.AddEntity(minotaur);
            }

            // Spawn a few goblins
            for (int i = 0; i < 8; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true); // Get a location that is walkable
                var goblin = new Goblin(posToSpawn, map);
                map.AddEntity(goblin);
            }

            // Spawn a few rats
            for (int i = 0; i < 4; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true); // Get a location that is walkable
                var rat = new Rat(posToSpawn, map);
                map.AddEntity(rat);
            }

            // Spawn player
            posToSpawn = map.WalkabilityView.RandomPosition(true);
            map.ControlledGameObject = new Player(posToSpawn);
            map.AddEntity(map.ControlledGameObject);

            return map;
        }

        private static IGameObject SpawnTerrain(Coord position, bool mapGenValue)
        {
            // Floor or wall. This could use the Factory system, or instantiate Floor and Wall classes, 
            // or something else if you prefer; this simplistic if-else is just used for example
            if (mapGenValue)
            {
                // Floor
                return new BasicTerrain(Color.SaddleBrown, Color.Black, '.', position,
                    isWalkable: true, isTransparent: true);
            }
            else
            {
                // Wall
                return new BasicTerrain(Color.RosyBrown, Color.Black, '#', position,
                    isWalkable: false, isTransparent: false);
            }
        }
    }
}
