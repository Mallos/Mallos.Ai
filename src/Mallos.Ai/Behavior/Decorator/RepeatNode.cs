namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that repeats itself for a specified amount of times.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class RepeatNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        private readonly int times;
        private readonly string timesKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatNode"/> class.
        /// </summary>
        /// <param name="child">The child node.</param>
        /// <param name="times">The amount of times this will be executed at once.</param>
        /// <param name="timesKey">The blackboard property key that will contain the how many times it has executed.</param>
        public RepeatNode(BehaviorTreeNode child, int times, string timesKey = null)
        {
            if (times <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(times));
            }

            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.times = times;
            this.timesKey = timesKey;
        }

        /// <summary>
        /// Gets the child node.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            var lastResult = BehaviorReturnCode.Failure;
            for (int i = 1; i <= this.times; i++)
            {
                if (!string.IsNullOrWhiteSpace(this.timesKey))
                {
                    blackboard.Properties[this.timesKey] = i;
                }

                lastResult = this.Child.Execute(blackboard);
            }

            return lastResult;
        }
    }
}
