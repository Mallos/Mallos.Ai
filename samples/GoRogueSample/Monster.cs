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
                    new ActionNode(blackboard =>
                    {
                        // Check if we can see the player.
                        if (false) // FIXME: Add logic
                        {
                            blackboard.Properties["WalkTo"] = new Coord(0, 0);
                            blackboard.Properties["SeePlayer"] = true;
                        }
                        else
                        {
                            blackboard.Properties["SeePlayer"] = false;
                        }

                        return BehaviorReturnCode.Success;
                    }),
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
