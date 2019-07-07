namespace Mallos.Ai.Behavior.Decorator
{
    using System;
    using System.Threading;
    using Xunit;

    public class TimeoutNodeTest
    {
        [Fact]
        public void ExecuteSlow()
        {
            var myblackboard = new Blackboard();

            var node = new TimeoutNode(new Task.ActionNode(blackboard =>
            {
                Thread.Sleep(600);
                return BehaviorReturnCode.Success;
            }), TimeSpan.FromSeconds(0.5f));

            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Failure, result);
        }

        [Fact]
        public void ExecuteFast()
        {
            var myblackboard = new Blackboard();

            var node = new TimeoutNode(new Task.ActionNode(blackboard =>
            {
                return BehaviorReturnCode.Success;
            }), TimeSpan.FromSeconds(0.5f));

            var result = node.Execute(myblackboard);

            Assert.Equal(BehaviorReturnCode.Success, result);
        }
    }
}
