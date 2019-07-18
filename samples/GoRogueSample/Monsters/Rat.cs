using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;

namespace GoRogueSample.Monsters
{
    class Rat : Monster
    {
        public Rat(Coord posToSpawn, Map map)
            : base(posToSpawn, map, Color.White, 'r')
        {

        }
    }
}
