namespace Mallos.Ai.Behavior.Composite
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Execute all the nodes in order, if one fails then return.
    /// </summary>
    /// <remarks>
    /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
    /// - Returns Success if a behavior component returns Success.
    /// - Returns Running if a behavior component returns Failure or Running.
    /// - Returns Failure if all behavior components returned Failure or an error has occured.
    /// </remarks>
    [BehaviorCategory(BehaviorCategory.Composite)]
    public class SequenceNode : BehaviorTreeNode, IBehaviorTreeNodeChildren
    {
        private short sequence = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceNode"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public SequenceNode(params BehaviorTreeNode[] children)
        {
            this.Children = new List<BehaviorTreeNode>(children);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<BehaviorTreeNode> Children { get; }

        /// <summary>
        /// Add a new child.
        /// </summary>
        /// <param name="node">The new child.</param>
        public void Add(BehaviorTreeNode node)
        {
            this.Children.Add(node);
        }

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
            while (this.sequence < this.Children.Count)
            {
                switch (this.Children[this.sequence].Execute(blackboard))
                {
                    case BehaviorReturnCode.Failure:
                        this.sequence++;
                        return BehaviorReturnCode.Running;

                    case BehaviorReturnCode.Success:
                        this.sequence = 0;
                        return BehaviorReturnCode.Success;

                    case BehaviorReturnCode.Running:
                        return BehaviorReturnCode.Running;

                    default:
                        this.sequence++;
                        return BehaviorReturnCode.Failure;
                }
            }

            this.sequence = 0;
            return BehaviorReturnCode.Running;
        }
    }
}
