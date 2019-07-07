namespace Mallos.Ai.Behavior.Composite
{
    using Xunit;

    public class RandomSelectorNodeTest
    {
        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard();

            var node = new RandomSelectorNode(
                null,
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

            var result1 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result1);
            Assert.IsAssignableFrom<int>(myblackboard.Properties["Value"]);
            myblackboard.Properties["Value"] = null;

            var result2 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result2);
            Assert.IsAssignableFrom<int>(myblackboard.Properties["Value"]);
            myblackboard.Properties["Value"] = null;

            var result3 = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result3);
            Assert.IsAssignableFrom<int>(myblackboard.Properties["Value"]);
            myblackboard.Properties["Value"] = null;
        }
    }
}
