namespace Mallos.Ai.Behavior.Task
{
    using GoRogue;

    public class NavigateNode : BehaviorTreeNode
    {
        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard.Properties.ContainsKey("WalkTo") &&
                blackboard is RogueBlackboard rb)
            {
                var walkTo = (Coord)blackboard.Properties["WalkTo"];
                if (walkTo == rb.Entity.Position)
                {
                    return BehaviorReturnCode.Success;
                }

                var path = rb.Map.AStar.ShortestPath(rb.Entity.Position, walkTo);
                if (path != null)
                {
                    var newPosition = path.GetStepWithStart(1);
                    rb.Entity.Position = newPosition;
                    return path.Length > 1 ? BehaviorReturnCode.Running : BehaviorReturnCode.Success;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
