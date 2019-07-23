namespace Mallos.Ai.Behavior.Decorator
{
    using System;

    /// <summary>
    /// A node that executes after a given amount of time in seconds have passed.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Decorator)]
    [Serializable]
    public class TimerNode : BehaviorTreeNode, IBehaviorTreeNodeChild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerNode"/> class.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="sleepTime">The amount of time in seconds that we will wait.</param>
        /// <param name="durationKey">Blackboard Property key for storing the current duration.</param>
        /// <param name="failureCode">The code that will return when the duration is hit.</param>
        public TimerNode(
            BehaviorTreeNode child,
            float sleepTime,
            string durationKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Running)
        {
            this.Child = child ?? throw new ArgumentNullException(nameof(child));
            this.SleepTime = sleepTime;
            this.DurationKey = durationKey ?? Guid.ToString();
            this.FailureCode = failureCode;
        }

        /// <summary>
        /// Gets the child.
        /// </summary>
        public BehaviorTreeNode Child { get; }

        /// <summary>
        /// Gets the amount of time in seconds that we will wait.
        /// </summary>
        public float SleepTime { get; }

        /// <summary>
        /// Gets the Blackboard Property key for storing the current duration.
        /// </summary>
        public string DurationKey { get; }

        /// <summary>
        /// Gets the code that will return when the duration is hit.
        /// </summary>
        public BehaviorReturnCode FailureCode { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            blackboard.Increment(this.DurationKey, (float)blackboard.ElapsedTime.TotalSeconds);
            if (blackboard.GetProperty<float>(this.DurationKey) >= this.SleepTime)
            {
                blackboard.Properties[this.DurationKey] = 0.0f;
                return this.Child.Execute(blackboard);
            }

            return this.FailureCode;
        }
    }
}
