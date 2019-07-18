using GoRogue;
using GoRogue.GameFramework;
using Mallos.Ai;
using Mallos.Ai.Behavior;
using Mallos.Ai.Behavior.Composite;
using Mallos.Ai.Behavior.Decorator;
using Mallos.Ai.Behavior.Task;
using Microsoft.Xna.Framework;
using SadConsole;

namespace GoRogueSample.Monsters
{
    /// <summary>
    /// The base class for all Monsters in the world.
    /// </summary>
    abstract class Monster : BasicEntity
    {
        public RogueBlackboard AiBlackboard { get; }
        public BehaviorTree BehaviorTree { get; }

        public Monster(Coord posToSpawn, Map map, Color foreground, int glyph)
            : base(foreground, Color.Transparent, glyph, posToSpawn,
                  (int)MapLayer.MONSTERS, isWalkable: false, isTransparent: true)
        {
            // TODO: Can I use CurrentMap instead of passing map?
            this.AiBlackboard = new RogueBlackboard(map, this);
            this.BehaviorTree = CreateBehaviorTree();
        }

        public void WorldTick()
        {
            this.BehaviorTree.Execute(this.AiBlackboard);
        }

        protected virtual BehaviorTree CreateBehaviorTree()
        {
            return new BehaviorTree(
                new ParallelSequenceNode(
                    // Check if we can see the player.
                    new EnvironmentQueryNode(
                        entity => 5,                    // Entity View Radius.
                        entity => entity is Player,     // Check if it is a player.
                        spottedKey: "SeePlayer",        // Set a blackboard property with true or false.
                        spottedCoordKey: "WalkTo"       // Set a blackboard property with the found coords.
                    ),
                    new ConditionalNode(
                        blackboard => blackboard.GetProperty<bool>("SeePlayer"),
                        new NavigateNode("WalkTo"),     // Go to Player
                        new WanderNode()                // Wander around
                    )
                )
            );
        }
    }
}
