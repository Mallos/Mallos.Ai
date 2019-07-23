namespace Mallos.Ai.Behavior.Task
{
    /// <summary>
    /// A <see cref="BehaviorTreeNode"/> that will always
    /// return <see cref="BehaviorTreeNode.Failure"/>.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    [System.Serializable]
    public class AlwaysFailureNode : BehaviorTreeNode
    {
        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            return BehaviorReturnCode.Failure;
        }
    }
}
