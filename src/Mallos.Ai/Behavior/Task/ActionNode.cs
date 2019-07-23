namespace Mallos.Ai.Behavior.Task
{
    using System;

    /// <summary>
    /// A lambda node.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    public class ActionNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// </summary>
        /// <param name="function">The executed function.</param>
        public ActionNode(Func<Blackboard, BehaviorReturnCode> function)
        {
            this.Function = function ?? throw new ArgumentNullException(nameof(function));
        }

        /// <summary>
        /// Gets the function that is invoked.
        /// </summary>
        public Func<Blackboard, BehaviorReturnCode> Function { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            return this.Function.Invoke(blackboard);
        }
    }
}
