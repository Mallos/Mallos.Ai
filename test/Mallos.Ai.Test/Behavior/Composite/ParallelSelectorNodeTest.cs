namespace Mallos.Ai.Behavior.Composite
{
    using Xunit;

    public class ParallelSelectorNodeTest
    {
        [Fact]
        public void LastNodeSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new ParallelSelectorNode(
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 1;
                    return BehaviorReturnCode.Failure;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 2;
                    return BehaviorReturnCode.Failure;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 3;
                    return BehaviorReturnCode.Success;
                })
            );


            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result);
            Assert.Equal(3, (int)myblackboard.Properties["Value"]);
        }

        [Fact]
        public void MiddleNodeRunning()
        {
            var myblackboard = new Blackboard();

            var node = new ParallelSelectorNode(
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 1;
                    return BehaviorReturnCode.Failure;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 2;
                    return BehaviorReturnCode.Running;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 3;
                    return BehaviorReturnCode.Success;
                })
            );


            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Running, result);
            Assert.Equal(2, (int)myblackboard.Properties["Value"]);
        }
    }
}
