namespace Mallos.Ai.Behavior.Task
{
    /// <summary>
    /// A node that will sleep for a specified time
    /// and be active for a specified time.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    public class SleepNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SleepNode"/> class.
        /// </summary>
        /// <param name="sleepTime">The amount of time in seconds that this node will sleep for.</param>
        /// <param name="aliveTime">The amount of time in seconds that this node will be active for.</param>
        /// <param name="durationKey">Blackboard Property key for storing the current duration.</param>
        public SleepNode(
            float sleepTime,
            float aliveTime,
            string durationKey = null)
        {
            this.SleepTime = sleepTime;
            this.AliveTime = aliveTime;
            this.DurationKey = durationKey ?? Guid.ToString();
        }

        /// <summary>
        /// Gets the amount of time in seconds that this node will sleep for.
        /// </summary>
        public float SleepTime { get; }

        /// <summary>
        /// Gets the amount of time in seconds that this node will be active for.
        /// </summary>
        public float AliveTime { get; }

        /// <summary>
        /// Gets the Blackboard Property key for storing the current duration.
        /// </summary>
        public string DurationKey { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            var duration = blackboard.Increment(this.DurationKey, (float)blackboard.ElapsedTime.TotalSeconds) % (this.SleepTime + this.AliveTime);
            blackboard.Properties[this.DurationKey] = duration;
            return (duration < this.SleepTime) ? BehaviorReturnCode.Running : BehaviorReturnCode.Failure;
        }
    }
}
