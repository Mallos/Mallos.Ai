namespace Mallos.Ai.Behavior.Decorator
{
    using Xunit;
    using Xunit.Abstractions;

    public class CounterNodeTest
    {
        private readonly ITestOutputHelper output;

        public CounterNodeTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Execute()
        {
            var myblackboard = new Blackboard();
            var node = new CounterNode(new Task.AlwaysSuccessNode(), 5);

            for (int i = 1; i <= 10; i++)
            {
                var result = node.Execute(myblackboard);

                var expected = ((i % 5) == 0) ?
                    BehaviorReturnCode.Success :
                    BehaviorReturnCode.Running;

                output.WriteLine(
                    "result: {0}, expected: {1}, counts: {2}",
                    result, expected, i
                );

                Assert.Equal(expected, result);
            }
        }
    }
}
