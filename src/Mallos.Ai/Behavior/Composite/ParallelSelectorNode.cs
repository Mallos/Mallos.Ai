namespace Mallos.Ai.Behavior.Composite
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Selects a node to execute.
    /// </summary>
    /// <remarks>
    /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
    /// - Returns Success if a behavior component returns Success.
    /// - Returns Running if a behavior component returns Running.
    /// - Returns Failure if all behavior components returned Failure.
    /// </remarks>
    [BehaviorCategory(BehaviorCategory.Composite)]
    public class ParallelSelectorNode : BehaviorTreeNode, IBehaviorTreeNodeChildren
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelSelectorNode"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public ParallelSelectorNode(params BehaviorTreeNode[] children)
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
                        continue;

                    case BehaviorReturnCode.Success:
                        return BehaviorReturnCode.Success;

                    case BehaviorReturnCode.Running:
                        return BehaviorReturnCode.Running;

                    default:
                        continue;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
