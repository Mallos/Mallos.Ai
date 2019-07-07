namespace Mallos.Ai.Behavior.Task
{
    using Xunit;

    public class AlwaysFailureNodeTest
    {
        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard();
            var node = new AlwaysFailureNode();
            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Failure, result);
        }
    }
}
