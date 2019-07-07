namespace Mallos.Ai.Behavior.Composite
{
    using Xunit;

    public class SequenceNodeTest
    {
        [Fact]
        public void LastNodeSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new SequenceNode(
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

            var result1 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Running, result1);

            var result2 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Running, result2);

            var result3 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result3);
            Assert.Equal(3, (int)myblackboard.Properties["Value"]);
        }

        [Fact]
        public void MiddleNodeSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new SequenceNode(
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 1;
                    return BehaviorReturnCode.Failure;
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

            var result1 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Running, result1);

            var result2 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result2);
            Assert.Equal(2, (int)myblackboard.Properties["Value"]);
        }

        [Fact]
        public void StuckOnFirstNode()
        {
            var myblackboard = new Blackboard();

            var node = new SequenceNode(
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 1;
                    return BehaviorReturnCode.Running;
                }),
                new Task.ActionNode(blackboard =>
                {
                    blackboard.Properties["Value"] = 2;
                    return BehaviorReturnCode.Failure;
                })
            );

            var result1 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Running, result1);
            Assert.Equal(1, (int)myblackboard.Properties["Value"]);

            var result2 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Running, result2);
            Assert.Equal(1, (int)myblackboard.Properties["Value"]);
        }
    }
}
