namespace Mallos.Ai.Behavior.Task
{
    using System;

    [Obsolete("Not completely sure how this should work in the behavior tree.")]
    [BehaviorCategory(BehaviorCategory.Task)]
    public class PlayAnimationNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayAnimationNode"/> class.
        /// </summary>
        public PlayAnimationNode()
        {
            // TODO: Implement this
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            throw new NotImplementedException();
        }
    }
}
