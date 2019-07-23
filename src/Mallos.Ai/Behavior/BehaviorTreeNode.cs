namespace Mallos.Ai.Behavior
{
    using System;

    /// <summary>
    /// The base class for all behavior tree nodes.
    /// </summary>
    [Serializable]
    public abstract class BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorTreeNode"/> class.
        /// </summary>
        protected BehaviorTreeNode()
        {
            this.Guid = Guid.NewGuid();
        }

        /// <summary>
        /// Unique ID for this node.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Execute this behavior tree node.
        /// </summary>
        /// <param name="blackboard">The blackboard state.</param>
        /// <returns>The return code that defines if we should continue or not.</returns>
        public BehaviorReturnCode Execute(Blackboard blackboard)
        {
            return this.Behave(blackboard);
        }

        /// <summary>
        /// Execute this behavior tree node.
        /// </summary>
        /// <param name="blackboard">The blackboard state.</param>
        /// <returns>The return code that defines if we should continue or not.</returns>
        protected abstract BehaviorReturnCode Behave(Blackboard blackboard);
    }
}
