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
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationAtLocationNode"/> class.
        /// </summary>
        /// <param name="positionFunc">A function that returns the desired location.</param>
        /// <param name="atLocationKey">Blackboard Property key for storing if we are at that location.</param>
        /// <param name="failureCode">The code that will return if failed.</param>
        public NavigationAtLocationNode(
            Func<BasicEntity, Coord> positionFunc,
            string atLocationKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Failure)
        {
            this.Function = positionFunc;
            this.AtLocationKey = atLocationKey;
            this.FailureCode = failureCode;
        }

        /// <summary>
        /// Gets the function that returns the desired location.
        /// </summary>
        public Func<BasicEntity, Coord> Function { get; }

        /// <summary>
        /// Gets the blackboard Property key for storing if we are at that location.
        /// </summary>
        public string AtLocationKey { get; }

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
                if (rb.Entity.Position == desiredPosition)
                {
                    if (!string.IsNullOrWhiteSpace(this.AtLocationKey))
                    {
                        rb.Properties[this.AtLocationKey] = true;
                    }

                    return BehaviorReturnCode.Success;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(this.AtLocationKey))
                    {
                        rb.Properties[this.AtLocationKey] = false;
                    }

                    return this.FailureCode;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
