namespace Mallos.Ai.Behavior.Decorator
{
    using Xunit;

    public class RetryNodeTest
    {
        [Fact]
        public void Failure()
        {
            var myblackboard = new Blackboard();
            var node = new RetryNode(new Task.AlwaysFailureNode(), 10);
            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Failure, result);
        }

        [Fact]
        public void Success()
        {
            var myblackboard = new Blackboard();
            var node = new RetryNode(new Task.AlwaysSuccessNode(), 10);
            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Success, result);
        }

        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard();

            var node = new RetryNode(new Task.ActionNode(blackboard =>
            {
                var attempt = (int)blackboard.Properties["Attempt"];
                if (attempt >= 5)
                {
                    return BehaviorReturnCode.Success;
                }
                else
                {
                    return BehaviorReturnCode.Failure;
                }
            }), 10, "Attempt");

            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Success, result);
            Assert.Equal(5, (int)myblackboard.Properties["Attempt"]);
        }
    }
}
