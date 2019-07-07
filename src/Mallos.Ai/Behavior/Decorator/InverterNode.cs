namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that invert the given node result.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class InverterNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InverterNode"/> class.
        /// </summary>
        /// <param name="child">The child node.</param>
        public InverterNode(BehaviorTreeNode child)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
        }

        /// <summary>
        /// Gets the child node.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            switch (this.Child.Execute(blackboard))
            {
                case BehaviorReturnCode.Failure: return BehaviorReturnCode.Success;
                case BehaviorReturnCode.Success: return BehaviorReturnCode.Failure;
                default: return BehaviorReturnCode.Running;
            }
        }
    }
}
