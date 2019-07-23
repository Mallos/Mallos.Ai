namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that retries on <see cref="BehaviorReturnCode.Failure"/>.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class RetryNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryNode"/> class.
        /// </summary>
        /// <param name="child">The child node.</param>
        /// <param name="maxAttempts">The max attempts this node will be executed.</param>
        /// <param name="attemptKey">The blackboard property key that will contain the how many attempts have passed.</param>
        /// <param name="failureCode">The code that will return if exceed max attempts.</param>
        public RetryNode(
            BehaviorTreeNode child,
            int maxAttempts,
            string attemptKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Failure)
        {
            if (maxAttempts <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxAttempts));
            }

            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.MaxAttempts = maxAttempts;
            this.AttemptKey = attemptKey;
            this.FailureCode = failureCode;
        }

        /// <summary>
        /// Gets the child node.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <summary>
        /// Gets the max attempts this node will be executed.
        /// </summary>
        public int MaxAttempts { get; }

        /// <summary>
        /// Gets the blackboard property key that will contain the how many attempts have passed.
        /// </summary>
        public string AttemptKey { get; }

        /// <summary>
        /// Gets the code that will return if exceed max attempts.
        /// </summary>
        public BehaviorReturnCode FailureCode { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            for (int currentAttempts = 0; currentAttempts <= this.MaxAttempts; currentAttempts++)
            {
                if (!string.IsNullOrWhiteSpace(this.AttemptKey))
                {
                    blackboard.Properties[this.AttemptKey] = currentAttempts;
                }

                var result = this.Child.Execute(blackboard);
                if (result != BehaviorReturnCode.Failure)
                {
                    return result;
                }
            }

            return this.FailureCode;
        }
    }
}
