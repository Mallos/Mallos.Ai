namespace Mallos.Ai
{
    using Mallos.Ai.Behavior;
    using Mallos.Ai.Behavior.Composite;
    using Mallos.Ai.Behavior.Decorator;
    using Mallos.Ai.Behavior.Task;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public class SerializeTest
    {
        [Fact]
        public void Serialize()
        {
            var behaviorTree = new BehaviorTree(
                new ParallelSequenceNode(
                    new AlwaysSuccessNode()
                )
            );

            var bytes = SerializeObject(behaviorTree);

            var recontructed = DeserializeObject<BehaviorTree>(bytes);

            Assert.Equal(behaviorTree, recontructed);
        }

        [Fact]
        public void SerializeFunction()
        {
            var behaviorTree = new BehaviorTree(
                new ParallelSequenceNode(
                    new AlwaysSuccessNode(),
                    new ConditionalNode(
                        blackboard => true,
                        new AlwaysSuccessNode(),
                        new AlwaysFailureNode()
                    )
                )
            );

            var bytes = SerializeObject(behaviorTree);

            var recontructed = DeserializeObject<BehaviorTree>(bytes);

            Assert.Equal(behaviorTree, recontructed);
        }

        private byte[] SerializeObject(object value)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                return stream.ToArray();
            }
        }

        private T DeserializeObject<T>(byte[] value)
        {
            using (var stream = new MemoryStream(value))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
