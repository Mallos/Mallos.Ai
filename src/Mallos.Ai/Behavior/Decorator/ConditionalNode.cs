namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A <see cref="BehaviorTreeNode"/> that will execute
    /// the 2 nodes depending on the condition.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    [Serializable]
    public class ConditionalNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalNode"/> class.
        /// </summary>
        /// <param name="conditional">The conditional function.</param>
        /// <param name="trueNode">The node that will execute when true.</param>
        /// <param name="falseNode">The node that will execute when false.</param>
        public ConditionalNode(
            Func<Blackboard, bool> conditional,
            BehaviorTreeNode trueNode = null,
            BehaviorTreeNode falseNode = null)
        {
            this.Conditional = conditional ?? throw new ArgumentNullException(nameof(conditional));
            this.TrueNode = trueNode;
            this.FalseNode = falseNode;
        }

        /// <summary>
        /// Gets the conditional function that is invoked.
        /// </summary>
        public Func<Blackboard, bool> Conditional { get; }

        /// <summary>
        /// Gets the <see cref="BehaviorTreeNode"/> that will execute
        /// when the conditional is true.
        /// </summary>
        protected BehaviorTreeNode TrueNode { get; }

        /// <summary>
        /// Gets the <see cref="BehaviorTreeNode"/> that will execute
        /// when the conditional is false.
        /// </summary>
        protected BehaviorTreeNode FalseNode { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            switch (this.Conditional.Invoke(blackboard))
            {
                case true: return (this.TrueNode != null) ? this.TrueNode.Execute(blackboard) : BehaviorReturnCode.Success;
                default: return (this.FalseNode != null) ? this.FalseNode.Execute(blackboard) : BehaviorReturnCode.Success;
            }
        }
    }
}
