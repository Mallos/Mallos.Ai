namespace Mallos.Ai.Behavior.Task
{
    /// <summary>
    /// A node that makes the entity wander around with a max of 1 coord per update.
    /// </summary>
    public class WanderNode : BehaviorTreeNode
    {
        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var closePoint = rb.Map.FindClosePoint(rb.Entity.Position, 1);
                if (closePoint != rb.Entity.Position)
                {
                    rb.Entity.Position = closePoint;
                    return BehaviorReturnCode.Success;
                }
            }

            return BehaviorReturnCode.Failure;
        }
    }
}
