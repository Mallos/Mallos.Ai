namespace Mallos.Ai.Behavior.Task
{
    using System;

    /// <summary>
    /// A lambda node.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    public class ActionNode : BehaviorTreeNode
    {
        private readonly Func<Blackboard, BehaviorReturnCode> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// </summary>
        /// <param name="action">The executed function.</param>
        public ActionNode(Func<Blackboard, BehaviorReturnCode> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            return this.action.Invoke(blackboard);
        }
    }
}
