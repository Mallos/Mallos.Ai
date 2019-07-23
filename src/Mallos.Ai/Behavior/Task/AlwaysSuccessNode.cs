namespace Mallos.Ai.Behavior.Task
{
    /// <summary>
    /// A <see cref="BehaviorTreeNode"/> that will always
    /// return <see cref="BehaviorTreeNode.Success"/>.
    /// </summary>
    [BehaviorCategory(BehaviorCategory.Task)]
    [System.Serializable]
    public class AlwaysSuccessNode : BehaviorTreeNode
    {
        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            return BehaviorReturnCode.Success;
        }
    }
}
