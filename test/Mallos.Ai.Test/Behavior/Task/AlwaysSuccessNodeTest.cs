namespace Mallos.Ai.Behavior.Task
{
    using Xunit;

    public class AlwaysSuccessNodeTest
    {
        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard();
            var node = new AlwaysSuccessNode();
            var result = node.Execute(myblackboard);
            Assert.Equal(BehaviorReturnCode.Success, result);
        }
    }
}
