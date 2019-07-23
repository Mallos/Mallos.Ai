namespace Mallos.Ai.Behavior.Composite
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A node that selects the child depending on the passed function.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Composite)]
    public class SelectorQueryNode : BehaviorTreeNode, IBehaviorTreeNodeChildren
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorQueryNode"/> class.
        /// </summary>
        /// <param name="selector">The selector method.</param>
        /// <param name="children">The children.</param>
        public SelectorQueryNode(
            Func<SelectorQueryNode, Blackboard, int> selector,
            params BehaviorTreeNode[] children)
        {
            this.Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            this.Children = new List<BehaviorTreeNode>(children);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<BehaviorTreeNode> Children { get; }

        /// <summary>
        /// Gets the selector conditional function that is invoked.
        /// </summary>
        public Func<SelectorQueryNode, Blackboard, int> Selector { get; }

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
            var index = this.Selector.Invoke(this, blackboard);
            if (index < 0 || index >= this.Children.Count)
            {
                return BehaviorReturnCode.Failure;
            }

            return this.Children[index].Execute(blackboard);
        }
    }
}
