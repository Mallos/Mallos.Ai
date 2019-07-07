namespace Mallos.Ai.Behavior.Decorator
{
    using Xunit;

    public class ConditionalNodeTest
    {
        [Fact]
        public void ExecuteSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new ConditionalNode(
                blackboard => true,
                new Task.AlwaysSuccessNode(),
                new Task.AlwaysFailureNode()
            );

            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result);
        }

        [Fact]
        public void ExecuteFailure()
        {
            var myblackboard = new Blackboard();

            var node = new ConditionalNode(
                blackboard => false,
                new Task.AlwaysSuccessNode(),
                new Task.AlwaysFailureNode()
            );

            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Failure, result);
        }
    }
}
