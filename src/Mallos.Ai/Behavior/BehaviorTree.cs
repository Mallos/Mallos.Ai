namespace Mallos.Ai.Behavior
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The behavior tree.
    /// </summary>
    [Serializable]
    public class BehaviorTree : IEnumerable<BehaviorTreeNode>
    {
        private readonly BehaviorTreeNode rootNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorTree"/> class.
        /// </summary>
        /// <param name="rootNode">The root node.</param>
        public BehaviorTree(BehaviorTreeNode rootNode)
        {
            this.rootNode = rootNode ?? throw new ArgumentNullException(nameof(rootNode));
        }

        /// <summary>
        /// Execute the behavior tree.
        /// </summary>
        /// <param name="blackboard">The blackboard state.</param>
        public void Execute(Blackboard blackboard)
        {
            if (blackboard.OnBeforeExecute())
            {
                this.rootNode.Execute(blackboard);
                blackboard.OnAfterExecute();
            }
        }

        /// <summary>
        /// Returns the enumerator for the nodes.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<BehaviorTreeNode> GetEnumerator()
        {
            if (this.rootNode is IBehaviorTreeNodeChildren childrenNode)
            {
                return childrenNode.GetEnumerator();
            }

            return Enumerable.Empty<BehaviorTreeNode>().GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
