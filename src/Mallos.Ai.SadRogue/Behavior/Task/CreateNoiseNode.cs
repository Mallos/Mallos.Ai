namespace Mallos.Ai.Behavior.Task
{
    using System;

    // TODO: Maybe this should be more general create sense node.
    [Obsolete("Waiting on Map to support SenseMap")]
    [BehaviorCategory(BehaviorCategory.Task)]
    public class CreateNoiseNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNoiseNode"/> class.
        /// </summary>
        public CreateNoiseNode()
        {
            // TODO: Waiting on SadRogue
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            throw new NotImplementedException();
        }
    }
}
