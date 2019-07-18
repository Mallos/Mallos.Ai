using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;

namespace GoRogueSample.Monsters
{
    class Goblin : Monster
    {
        public static readonly Color DefaultColor = Color.Red;
        public static readonly char DefaultChar = 'g';

        public Goblin(Coord posToSpawn, Map map)
            : base(posToSpawn, map, DefaultColor, DefaultChar)
        {

        }
    }
}
