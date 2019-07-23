namespace Mallos.Ai.Behavior.Decorator
{
    using System;
    using GoRogue;
    using SadConsole;

    /// <summary>
    /// A node that check if a navigation path exist for the desired position.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class NavigationPathExistNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationPathExistNode"/> class.
        /// </summary>
        /// <param name="positionFunc">A function that returns the desired location.</param>
        /// <param name="hasPathKey">Blackboard Property key for storing if there is a possible navigation path.</param>
        /// <param name="failureCode">The code that will return if failed.</param>
        public NavigationPathExistNode(
            Func<BasicEntity, Coord> positionFunc,
            string hasPathKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Failure)
        {
            this.Function = positionFunc;
            this.HasPathKey = hasPathKey;
            this.FailureCode = failureCode;
        }

        /// <summary>
        /// Gets the function that is invoked.
        /// </summary>
        public Func<BasicEntity, Coord> Function { get; }

        /// <summary>
        /// Gets the blackboard Property key for storing if there is a possible navigation path.
        /// </summary>
        public string HasPathKey { get; }

        /// <summary>
        /// Gets the code that will return if failed.
        /// </summary>
        public BehaviorReturnCode FailureCode { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var desiredPosition = this.Function(rb.Entity);
                var path = rb.Map.AStar.ShortestPath(rb.Entity.Position, desiredPosition);

                if (path != null)
                {
                    if (!string.IsNullOrWhiteSpace(this.HasPathKey))
                    {
                        rb.Properties[this.HasPathKey] = true;
                    }

                    // TODO: Do we want a way to store the found path?
                    return BehaviorReturnCode.Success;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(this.HasPathKey))
                    {
                        rb.Properties[this.HasPathKey] = false;
                    }

                    return FailureCode;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
