namespace Mallos.Ai.Behavior.Task
{
    using System;
    using GoRogue;

    /// <summary>
    /// A node that navigates to the passed coord.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    public class NavigateNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateNode"/> class.
        /// </summary>
        /// <param name="walkToKey">Blackboard Property key for storing where we will walk.</param>
        public NavigateNode(string walkToKey)
        {
            if (string.IsNullOrWhiteSpace(walkToKey))
            {
                throw new ArgumentNullException(nameof(walkToKey));
            }

            this.WalkToKey = walkToKey;
        }

        /// <summary>
        /// Gets blackboard Property key for storing where we will walk.
        /// </summary>
        public string WalkToKey { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard.HasProperty<Coord>(this.WalkToKey) &&
                blackboard is RogueBlackboard rb)
            {
                var walkTo = blackboard.GetProperty<Coord>(this.WalkToKey);
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
