namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that will only execute a number of times.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class CounterNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CounterNode"/> class.
        /// </summary>
        /// <param name="child">The child node.</param>
        /// <param name="maxCount">The number of times this node will be exectued.</param>
        /// <param name="counterKey">Blackboard Property key for storing the current counter index.</param>
        public CounterNode(BehaviorTreeNode child, int maxCount, string counterKey = null)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.MaxCount = maxCount;
            this.CounterKey = counterKey ?? Guid.ToString();
        }

        /// <summary>
        /// Gets the child node.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <summary>
        /// Gets the max number of times this node will execute.
        /// </summary>
        public int MaxCount { get; }

        /// <summary>
        /// Gets the Blackboard Property key for storing the current counter index.
        /// </summary>
        public string CounterKey { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            var counter = blackboard.Increment(this.CounterKey);
            if (counter < this.MaxCount)
            {
                return BehaviorReturnCode.Running;
            }
            else
            {
                blackboard.Properties[this.CounterKey] = 0;
                return this.Child.Execute(blackboard);
            }
        }
    }
}
