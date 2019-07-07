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
        public CounterNode(BehaviorTreeNode child, int maxCount)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.MaxCount = maxCount;
            this.Counter = 0;
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
        /// Gets how many times this node have been executed.
        /// </summary>
        public int Counter { get; private set; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            this.Counter++;
            if (this.Counter < this.MaxCount)
            {
                return BehaviorReturnCode.Running;
            }
            else
            {
                this.Counter = 0;
                return this.Child.Execute(blackboard);
            }
        }
    }
}
