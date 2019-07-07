namespace Mallos.Ai.Behavior.Composite
{
    using Xunit;

    public class ParallelSequenceNodeTest
    {
        [Fact]
        public void FirstNodeFailure()
        {
            var myblackboard = new Blackboard();

            var node = new ParallelSequenceNode(
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
            Assert.Equal(BehaviorReturnCode.Failure, result);
            Assert.Equal(1, (int)myblackboard.Properties["Value"]);
        }

        [Fact]
        public void LastNodeFailure()
        {
            var myblackboard = new Blackboard();

            var node = new ParallelSequenceNode(
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 1;
                    return BehaviorReturnCode.Success;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 2;
                    return BehaviorReturnCode.Success;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 3;
                    return BehaviorReturnCode.Failure;
                })
            );


            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Failure, result);
            Assert.Equal(3, (int)myblackboard.Properties["Value"]);
        }

        [Fact]
        public void AllNodesSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new ParallelSequenceNode(
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 1;
                    return BehaviorReturnCode.Success;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 2;
                    return BehaviorReturnCode.Success;
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
    }
}
