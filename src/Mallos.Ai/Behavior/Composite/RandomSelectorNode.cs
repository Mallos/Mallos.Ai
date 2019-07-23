namespace Mallos.Ai.Behavior.Composite
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A node that execute a random child node.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Composite)]
    [Serializable]
    public class RandomSelectorNode : BehaviorTreeNode, IBehaviorTreeNodeChildren
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomSelectorNode"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        /// <param name="function">The random function.</param>
        public RandomSelectorNode(
            Func<Blackboard, (int min, int max), int> function = null,
            params BehaviorTreeNode[] children)
        {
            this.Children = new List<BehaviorTreeNode>(children);
            this.Function = function ?? RandomNode;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<BehaviorTreeNode> Children { get; }

        /// <summary>
        /// Gets the function that is invoked.
        /// </summary>
        public Func<Blackboard, (int min, int max), int> Function { get; }

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
            var index = this.Function.Invoke(blackboard, (0, this.Children.Count - 1));
            if (index < 0 || index >= this.Children.Count)
            {
                return BehaviorReturnCode.Failure;
            }

            return this.Children[index].Execute(blackboard);
        }

        private static int RandomNode(
            Blackboard blackboard, (int min, int max) range)
        {
            var random = new Random(DateTime.Now.Millisecond);
            return random.Next(range.min, range.max);
        }
    }
}
