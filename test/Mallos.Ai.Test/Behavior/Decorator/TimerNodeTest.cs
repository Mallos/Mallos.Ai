namespace Mallos.Ai.Behavior.Decorator
{
    using System;
    using Xunit;
    using Xunit.Abstractions;

    public class TimerNodeTest
    {
        private readonly ITestOutputHelper output;

        public TimerNodeTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard
            {
                // The elapsed time will only tick 0.1 seconds every update.
                ElapsedTime = TimeSpan.FromSeconds(0.1f)
            };

            var node = new TimerNode(new Task.AlwaysSuccessNode(), 1.0f);

            for (float elapsedTime = 0.1f; elapsedTime <= 2.5f; elapsedTime += 0.1f)
            {
                var result = node.Execute(myblackboard);

                // FIXME: Add some simpler way of testing this.
                var expected = (Math.Abs(elapsedTime - 1.0f) < 0.001 ||
                                Math.Abs(elapsedTime - 2.0f) < 0.001) ?
                    BehaviorReturnCode.Success :
                    BehaviorReturnCode.Running;

                output.WriteLine(
                    "result: {0}, expected: {1}, elapsedTime: {2}",
                    result, expected, elapsedTime
                );

                Assert.Equal(expected, result);
            }
        }
    }
}
