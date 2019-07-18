using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;

namespace GoRogueSample.Monsters
{
    class Minotaur : Monster
    {
        public Minotaur(Coord posToSpawn, Map map)
            : base(posToSpawn, map, Color.Red, 'm')
        {

        }
    }
}
