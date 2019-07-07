namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that executes after a given amount of time in seconds have passed.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    public class TimerNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        private readonly float sleepTime;
        private readonly BehaviorReturnCode failureCode;

        private float duration = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerNode"/> class.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="sleepTime">The amount of time in seconds that we will wait.</param>
        /// <param name="failureCode">The code that will return if exceed max attempts.</param>
        public TimerNode(
            BehaviorTreeNode child,
            float sleepTime,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Running)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.sleepTime = sleepTime;
            this.failureCode = failureCode;
        }

        /// <summary>
        /// Gets the child.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            this.duration += (float)blackboard.ElapsedTime.TotalSeconds;
            if (this.duration >= this.sleepTime)
            {
                this.duration = 0;
                return this.Child.Execute(blackboard);
            }

            return this.failureCode;
        }
    }
}
