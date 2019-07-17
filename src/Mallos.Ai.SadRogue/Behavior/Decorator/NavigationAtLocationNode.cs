namespace Mallos.Ai.Behavior.Decorator
{
    using System;
    using GoRogue;
    using SadConsole;

    /// <summary>
    /// A node that check if the entity is at the current location.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class NavigationAtLocationNode : BehaviorTreeNode
    {
        private readonly Func<BasicEntity, Coord> positionFunc;
        private readonly string atLocationKey;
        private readonly BehaviorReturnCode failureCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationAtLocationNode"/> class.
        /// </summary>
        /// <param name="positionFunc">A function that returns the desired location.</param>
        /// <param name="atLocationKey">Blackboard Property key for storing if we are at that location.</param>
        /// <param name="failureCode">The code that will return if exceed max attempts.</param>
        public NavigationAtLocationNode(
            Func<BasicEntity, Coord> positionFunc,
            string atLocationKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Failure)
        {
            this.positionFunc = positionFunc;
            this.atLocationKey = atLocationKey;
            this.failureCode = failureCode;
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var desiredPosition = this.positionFunc(rb.Entity);
                if (rb.Entity.Position == desiredPosition)
                {
                    if (!string.IsNullOrWhiteSpace(atLocationKey))
                    {
                        rb.Properties[atLocationKey] = true;
                    }

                    return BehaviorReturnCode.Success;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(atLocationKey))
                    {
                        rb.Properties[atLocationKey] = false;
                    }

                    return failureCode;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
