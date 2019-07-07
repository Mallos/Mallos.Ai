namespace Mallos.Ai.Behavior.Decorator
{
    using Xunit;

    public class InverterNodeTest
    {
        [Fact]
        public void ExecuteSuccess()
        {
            var myblackboard = new Blackboard();
            var node = new InverterNode(new Task.AlwaysFailureNode());
            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result);
        }

        [Fact]
        public void ExecuteFailure()
        {
            var myblackboard = new Blackboard();
            var node = new InverterNode(new Task.AlwaysSuccessNode());
            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Failure, result);
        }
    }
}
