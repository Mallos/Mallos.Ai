namespace Mallos.Ai.Behavior.Task
{
    using System;

    [Obsolete("Waiting on Map to support SenseMap")]
    [BehaviorCategory(BehaviorCategory.Task)]
    public class SenseNode : BehaviorTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SenseNode"/> class.
        /// </summary>
        public SenseNode()
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
