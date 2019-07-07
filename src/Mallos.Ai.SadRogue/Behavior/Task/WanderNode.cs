namespace Mallos.Ai.Behavior.Task
{
    public class WanderNode : BehaviorTreeNode
    {
        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var closePoint = rb.Map.FindClosePoint(rb.Entity.Position);
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
