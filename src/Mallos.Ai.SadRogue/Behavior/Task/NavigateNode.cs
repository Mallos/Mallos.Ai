namespace Mallos.Ai.Behavior.Task
{
    using System;
    using GoRogue;

    public class NavigateNode : BehaviorTreeNode
    {
        private readonly string walkToKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateNode"/> class.
        /// </summary>
        /// <param name="walkToKey"></param>
        public NavigateNode(string walkToKey)
        {
            if (string.IsNullOrWhiteSpace(walkToKey))
            {
                throw new ArgumentNullException(nameof(walkToKey));
            }

            this.walkToKey = walkToKey;
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard.HasProperty<Coord>(this.walkToKey) &&
                blackboard is RogueBlackboard rb)
            {
                var walkTo = blackboard.GetProperty<Coord>(this.walkToKey);
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
