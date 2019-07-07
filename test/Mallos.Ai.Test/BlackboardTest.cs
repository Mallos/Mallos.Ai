namespace Mallos.Ai
{
    using System;
    using Xunit;

    public class BlackboardTest
    {
        [Fact]
        public void GetSetElapsedTime()
        {
            var blackboard = new Blackboard();

            blackboard.ElapsedTime = new TimeSpan(0, 0, 0);
            Assert.Equal(new TimeSpan(0, 0, 0), blackboard.ElapsedTime);

            blackboard.ElapsedTime = new TimeSpan(1, 0, 0);
            Assert.Equal(new TimeSpan(1, 0, 0), blackboard.ElapsedTime);
        }

        [Fact]
        public void GetSetProperties()
        {
            var blackboard = new Blackboard();

            blackboard.Properties["Value"] = 1;
            Assert.Equal(1, blackboard.Properties["Value"]);

            blackboard.Properties["Value"] = 2;
            Assert.Equal(2, blackboard.Properties["Value"]);
        }

        [Fact]
        public void IsRunning()
        {
            var blackboard = new Blackboard();
            var behaviorTree = new Behavior.BehaviorTree(new Behavior.Task.AlwaysSuccessNode());

            // FIXME: Only way to really test this is to run it on a different thread.

            Assert.False(blackboard.IsRunning);
            behaviorTree.Execute(blackboard);
            Assert.False(blackboard.IsRunning);
        }
    }
}
