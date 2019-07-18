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
        private readonly Func<BasicEntity, Coord> positionFunc;
        private readonly string hasPathKey;
        private readonly BehaviorReturnCode failureCode;

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
            this.positionFunc = positionFunc;
            this.hasPathKey = hasPathKey;
            this.failureCode = failureCode;
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var desiredPosition = this.positionFunc(rb.Entity);
                var path = rb.Map.AStar.ShortestPath(rb.Entity.Position, desiredPosition);

                if (path != null)
                {
                    if (!string.IsNullOrWhiteSpace(hasPathKey))
                    {
                        rb.Properties[hasPathKey] = true;
                    }

                    // TODO: Do we want a way to store the found path?
                    return BehaviorReturnCode.Success;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(hasPathKey))
                    {
                        rb.Properties[hasPathKey] = false;
                    }

                    return failureCode;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
