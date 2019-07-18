using GoRogue;
using GoRogue.GameFramework;
using Mallos.Ai.Behavior;
using Microsoft.Xna.Framework;

namespace GoRogueSample.Monsters
{
    class Minotaur : Monster
    {
        public static readonly Color DefaultColor = Color.Red;
        public static readonly char DefaultChar = 'm';

        public Minotaur(Coord posToSpawn, Map map)
            : base(posToSpawn, map, DefaultColor, DefaultChar)
        {
            this.FOVRadius = 3;
        }

        protected override BehaviorTree CreateBehaviorTree()
        {
            // Minotaur is very aggressive
            return base.CreateBehaviorTree();
        }
    }
}
