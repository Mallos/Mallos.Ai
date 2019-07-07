namespace Mallos.Ai.Behavior.Task
{
    using System;
    using Xunit;
    using Xunit.Abstractions;

    public class SleepNodeTest
    {
        private readonly ITestOutputHelper output;

        public SleepNodeTest(ITestOutputHelper output)
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

            var node = new SleepNode(1.0f, 1.0f);

            for (float elapsedTime = 0.1f; elapsedTime <= 2.5f; elapsedTime += 0.1f)
            {
                var result = node.Execute(myblackboard);

                // NOTE: I didn't want to make the test to complicated.

                if (elapsedTime >= 2.0f)
                {
                    var expected = BehaviorReturnCode.Running;

                    output.WriteLine(
                        "result: {0}, expected: {1}, elapsedTime: {2}",
                        result, expected, elapsedTime
                    );

                    Assert.Equal(expected, result);
                }
                else
                {
                    var expected = (elapsedTime <= 1.0f) ?
                        BehaviorReturnCode.Running :
                        BehaviorReturnCode.Failure;

                    output.WriteLine(
                        "result: {0}, expected: {1}, elapsedTime: {2}",
                        result, expected, elapsedTime
                    );

                    Assert.Equal(expected, result);
                }
            }
        }
    }
}
