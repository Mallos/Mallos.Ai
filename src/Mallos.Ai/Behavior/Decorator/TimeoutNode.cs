namespace Mallos.Ai.Behavior.Decorator
{
    using System;
    using System.Threading;
    using TTask = System.Threading.Tasks.Task;

    /// <summary>
    /// A node that wait for a specified time and if the child
    /// doesn't complete within the time then fail the node.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    [Serializable]
    public class TimeoutNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeoutNode"/> class.
        /// </summary>
        /// <param name="child">The child node.</param>
        /// <param name="timeout">The amount of time this node has until it times out.</param>
        public TimeoutNode(BehaviorTreeNode child, TimeSpan timeout)
        {
            if (timeout == TimeSpan.MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.Timeout = timeout;
        }

        /// <summary>
        /// Gets the child node.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <summary>
        /// Gets the amount of time this node has until it times out.
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = TTask.Run(() => this.Child.Execute(blackboard), cancellationTokenSource.Token);

            if (task.Wait(this.Timeout))
            {
                return task.Result;
            }
            else
            {
                cancellationTokenSource.Cancel();
                return BehaviorReturnCode.Failure;
            }
        }
    }
}
