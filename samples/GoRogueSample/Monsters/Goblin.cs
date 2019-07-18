using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;

namespace GoRogueSample.Monsters
{
    class Goblin : Monster
    {
        public Goblin(Coord posToSpawn, Map map)
            : base(posToSpawn, map, Color.Red, 'g')
        {

        }
    }
}
