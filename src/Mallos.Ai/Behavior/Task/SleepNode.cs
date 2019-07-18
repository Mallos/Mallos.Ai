namespace Mallos.Ai.Behavior.Task
{
    /// <summary>
    /// A node that will sleep for a specified time
    /// and be active for a specified time.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    public class SleepNode : BehaviorTreeNode
    {
        private readonly float sleepTime;
        private readonly float aliveTime;

        private float duration = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SleepNode"/> class.
        /// </summary>
        /// <param name="sleepTime">The amount of time in seconds that this node will sleep for.</param>
        /// <param name="aliveTime">The amount of time in seconds that this node will be active for.</param>
        public SleepNode(float sleepTime, float aliveTime)
        {
            // TODO: Can this be based on the blackboard?
            this.sleepTime = sleepTime;
            this.aliveTime = aliveTime;
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            this.duration = (this.duration + (float)blackboard.ElapsedTime.TotalSeconds) % (this.sleepTime + this.aliveTime);
            return (this.duration < this.sleepTime) ? BehaviorReturnCode.Running : BehaviorReturnCode.Failure;
        }
    }
}
