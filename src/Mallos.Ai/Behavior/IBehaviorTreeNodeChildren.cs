namespace Mallos.Ai.Behavior
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface that exposes a <see cref="BehaviorTreeNode"/> that has many children.
    /// </summary>
    public interface IBehaviorTreeNodeChildren
        : IEnumerable<BehaviorTreeNode>
    {
        /// <summary>
        /// Gets the childrens.
        /// </summary>
        List<BehaviorTreeNode> Children { get; }
    }
}
