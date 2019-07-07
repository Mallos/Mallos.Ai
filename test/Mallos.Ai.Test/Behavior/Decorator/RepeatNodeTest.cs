namespace Mallos.Ai.Behavior.Decorator
{
    using Xunit;

    public class RepeatNodeTest
    {
        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard();
            myblackboard.Properties["Value"] = 0;

            var node = new RepeatNode(new Task.ActionNode(blackboard =>
            {
                blackboard.Properties["Value"] = (int)blackboard.Properties["Value"] + 1;
                return BehaviorReturnCode.Success;
            }), 10);

            Assert.Equal(0, (int)myblackboard.Properties["Value"]);
            node.Execute(myblackboard);
            Assert.Equal(10, (int)myblackboard.Properties["Value"]);
        }

        [Fact]
        public void ExecuteWithKey()
        {
            var myblackboard = new Blackboard();
            myblackboard.Properties["Value"] = 0;

            var node = new RepeatNode(new Task.AlwaysSuccessNode(), 10, "Value");

            Assert.Equal(0, (int)myblackboard.Properties["Value"]);
            node.Execute(myblackboard);
            Assert.Equal(10, (int)myblackboard.Properties["Value"]);
        }
    }
}
