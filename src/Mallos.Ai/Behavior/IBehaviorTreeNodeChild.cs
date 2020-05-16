namespace Mallos.Ai.Behavior
{
    /// <summary>
    /// An interface that exposes a <see cref="BehaviorTreeNode"/> that have a child.
    /// </summary>
    public interface IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Gets the child node.
        /// </summary>
        BehaviorTreeNode Child { get; }
    }
}
