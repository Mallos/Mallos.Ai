using GoRogue;
using GoRogue.GameFramework;
using Mallos.Ai.Behavior;
using Mallos.Ai.Behavior.Task;
using Microsoft.Xna.Framework;

namespace GoRogueSample.Monsters
{
    class Rat : Monster
    {
        public static readonly Color DefaultColor = Color.White;
        public static readonly char DefaultChar = 'r';

        public Rat(Coord posToSpawn, Map map)
            : base(posToSpawn, map, DefaultColor, DefaultChar)
        {

        }

        protected override BehaviorTree CreateBehaviorTree()
        {
            // The rat is quite stupid and will just wander around without a purpose.
            return new BehaviorTree(
                new WanderNode()
            );
        }
    }
}
