namespace Mallos.Ai.Behavior.Decorator
{
    using Xunit;

    public class RandomDecoratorNodeTest
    {
        [Fact]
        public void ExecuteSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new RandomDecoratorNode(
                new Task.AlwaysSuccessNode(),
                1.0f
            );

            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result);
        }

        [Fact]
        public void ExecuteFailure()
        {
            var myblackboard = new Blackboard();

            var node = new RandomDecoratorNode(
                new Task.AlwaysSuccessNode(),
                0.0f
            );

            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Failure, result);
        }
    }
}
