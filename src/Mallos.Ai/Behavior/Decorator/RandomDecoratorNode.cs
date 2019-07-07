namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that might execute the child node with probability function.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class RandomDecoratorNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        private readonly Func<Blackboard, float> randomFunc;
        private readonly BehaviorReturnCode failureCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDecoratorNode"/> class.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="probability">The probability that it will execute.</param>
        /// <param name="randomFunc">The random function.</param>
        /// <param name="failureCode">The code that will return if exceed max attempts.</param>
        public RandomDecoratorNode(
            BehaviorTreeNode child,
            float probability,
            Func<Blackboard, float> randomFunc = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Failure)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.Probability = probability;
            this.randomFunc = randomFunc ?? RandomProbability;
            this.failureCode = failureCode;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <summary>
        /// Gets the probability.
        /// </summary>
        public float Probability { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            var number = this.randomFunc.Invoke(blackboard);
            if (number <= this.Probability)
            {
                return this.Child.Execute(blackboard);
            }

            return this.failureCode;
        }

        private static float RandomProbability(Blackboard blackboard)
        {
            var random = new Random(DateTime.Now.Millisecond);
            return (float)random.NextDouble();
        }
    }
}
