using GoRogue;
using GoRogue.GameFramework;
using Mallos.Ai;
using Mallos.Ai.Behavior;
using Mallos.Ai.Behavior.Composite;
using Mallos.Ai.Behavior.Decorator;
using Mallos.Ai.Behavior.Task;
using Microsoft.Xna.Framework;
using SadConsole;

namespace GoRogueSample
{
    class Monster : BasicEntity
    {
        public RogueBlackboard AiBlackboard;
        public BehaviorTree BehaviorTree;

        public Monster(Coord posToSpawn, Map map)
            : base(Color.Red, Color.Transparent, 'g', posToSpawn,
                  (int)MapLayer.MONSTERS, isWalkable: true, isTransparent: true)
        {
            this.AiBlackboard = new RogueBlackboard(map, this);
            this.BehaviorTree = CreateBehaviorTree();
        }

        public void UpdateAI()
        {
            this.BehaviorTree.Execute(this.AiBlackboard);
        }

        private BehaviorTree CreateBehaviorTree()
        {
            return new BehaviorTree(
                new ParallelSequenceNode(
                    // Check if we can see the player.
                    new SpotEntityNode(
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
