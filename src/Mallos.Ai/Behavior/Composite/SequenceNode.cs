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
    [System.Serializable]
    public class SequenceNode : BehaviorTreeNode, IBehaviorTreeNodeChildren
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceNode"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public SequenceNode(params BehaviorTreeNode[] children)
        {
            this.SequenceKey = Guid.ToString();
            this.Children = new List<BehaviorTreeNode>(children);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceNode"/> class.
        /// </summary>
        /// <param name="sequenceKey">Blackboard Property key for storing the current sequence.</param>
        /// <param name="children">The children.</param>
        public SequenceNode(string sequenceKey, params BehaviorTreeNode[] children)
        {
            this.SequenceKey = sequenceKey;
            this.Children = new List<BehaviorTreeNode>(children);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<BehaviorTreeNode> Children { get; }

        /// <summary>
        /// Gets the Blackboard Property key for storing the current sequence.
        /// </summary>
        public string SequenceKey { get; }

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
            if (!blackboard.HasProperty<int>(SequenceKey))
            {
                blackboard.Properties[SequenceKey] = 0;
            }

            var sequence = blackboard.GetProperty<int>(SequenceKey);
            while (sequence < this.Children.Count)
            {
                switch (this.Children[sequence].Execute(blackboard))
                {
                    case BehaviorReturnCode.Failure:
                        blackboard.Properties[SequenceKey] = sequence + 1;
                        return BehaviorReturnCode.Running;

                    case BehaviorReturnCode.Success:
                        blackboard.Properties[SequenceKey] = 0;
                        return BehaviorReturnCode.Success;

                    case BehaviorReturnCode.Running:
                        return BehaviorReturnCode.Running;

                    default:
                        blackboard.Properties[SequenceKey] = sequence + 1;
                        return BehaviorReturnCode.Failure;
                }
            }

            blackboard.Properties[SequenceKey] = 0;
            return BehaviorReturnCode.Running;
        }
    }
}
