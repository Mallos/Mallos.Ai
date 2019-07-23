namespace Mallos.Ai.Behavior.Composite
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Execute all the nodes in order, if one fails then return.
    /// </summary>
    /// <remarks>
    /// attempts to run the behaviors all in one cycle.
    /// - Returns Success when all are successful.
    /// - Returns Failure if one behavior fails or an error occurs.
    /// - Does not Return Running.
    /// </remarks>
    [BehaviorCategory(BehaviorCategory.Composite)]
    public class ParallelSequenceNode : BehaviorTreeNode, IBehaviorTreeNodeChildren
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelSequenceNode"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public ParallelSequenceNode(params BehaviorTreeNode[] children)
        {
            this.Children = new List<BehaviorTreeNode>(children);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<BehaviorTreeNode> Children { get; }

        /// <summary>
        /// Returns the enumerator for the nodes.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<BehaviorTreeNode> GetEnumerator()
        {
            return this.Children.GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator for the nodes.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Children.GetEnumerator();
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            for (int i = 0; i < this.Children.Count; i++)
            {
                switch (this.Children[i].Execute(blackboard))
                {
                    case BehaviorReturnCode.Failure:
                        return BehaviorReturnCode.Failure;

                    case BehaviorReturnCode.Success:
                        continue;

                    case BehaviorReturnCode.Running:
                        continue;

                    default:
                        return BehaviorReturnCode.Success;
                }
            }

            return BehaviorReturnCode.Success;
        }
    }
}
