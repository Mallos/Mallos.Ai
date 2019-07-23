namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that might execute the child node with probability function.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    [Serializable]
    public class RandomDecoratorNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDecoratorNode"/> class.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="probability">The probability that it will execute.</param>
        /// <param name="function">The random function.</param>
        /// <param name="failureCode">The code that will return if it wont execute.</param>
        public RandomDecoratorNode(
            BehaviorTreeNode child,
            float probability,
            Func<Blackboard, float> function = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Failure)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.Probability = probability;
            this.Function = function ?? RandomProbability;
            this.FailureCode = failureCode;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <summary>
        /// Gets the random function that determine if it should execute.
        /// </summary>
        public Func<Blackboard, float> Function { get; }

        /// <summary>
        /// Gets the probability.
        /// </summary>
        public float Probability { get; }

        /// <summary>
        /// Gets the code that will return if it wont execute.
        /// </summary>
        public BehaviorReturnCode FailureCode { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            var number = this.Function.Invoke(blackboard);
            if (number <= this.Probability)
            {
                return this.Child.Execute(blackboard);
            }

            return this.FailureCode;
        }

        private static float RandomProbability(Blackboard blackboard)
        {
            var random = new Random(DateTime.Now.Millisecond);
            return (float)random.NextDouble();
        }
    }
}
