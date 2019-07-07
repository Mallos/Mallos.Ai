namespace Mallos.Ai.Behavior.Composite
{
    using Xunit;

    public class SelectorQueryNodeTest
    {
        [Fact]
        public void ExecuteSuccess()
        {
            var myblackboard = new Blackboard();

            var node = new SelectorQueryNode(
                (sender, blackboard) => 1,
                new Task.AlwaysFailureNode(),
                new Task.AlwaysSuccessNode()
            );

            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result);
        }

        [Fact]
        public void ExecuteFailure()
        {
            var myblackboard = new Blackboard();

            var node = new SelectorQueryNode(
                (sender, blackboard) => 0,
                new Task.AlwaysFailureNode(),
                new Task.AlwaysSuccessNode()
            );

            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Failure, result);
        }
    }
}
