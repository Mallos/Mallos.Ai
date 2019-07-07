namespace Mallos.Ai.Behavior.Task
{
    using Xunit;

    public class ActionNodeTest
    {
        [Fact]
        public void ExecuteSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new ActionNode(blackboard =>
            {
                blackboard.Properties["Value"] = "abc";
                return BehaviorReturnCode.Success;
            });

            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Success, result);
            Assert.Equal("abc", myblackboard.Properties["Value"]);
        }

        [Fact]
        public void ExecuteFailure()
        {
            var myblackboard = new Blackboard();

            var node = new ActionNode(blackboard =>
            {
                blackboard.Properties["Value"] = "abc";
                return BehaviorReturnCode.Failure;
            });

            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Failure, result);
            Assert.Equal("abc", myblackboard.Properties["Value"]);
        }
    }
}
