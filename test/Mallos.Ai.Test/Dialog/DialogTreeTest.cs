namespace Mallos.Ai.Dialog
{
    using Xunit;

    public class DialogTreeTest
    {
        [Fact]
        public void RunTwoNodes()
        {
            var helloText = "Hello there! How are you?";
            var goodbyeText = "Good, goodbye!";

            var dialogTree = new DialogTree();
            var node1 = dialogTree.AddNode(helloText);
            var node2 = dialogTree.AddChoice(goodbyeText);
            dialogTree.AddLink(node1, node2);

            var runner = new DialogTreeRunner(dialogTree);

            Assert.True(runner.IsActive);
            Assert.False(runner.IsChoice);
            Assert.Equal(helloText, runner.CurrentOptions[0]);

            runner.Next();

            Assert.True(runner.IsActive);
            Assert.True(runner.IsChoice);
            Assert.Equal(goodbyeText, runner.CurrentOptions[0]);

            runner.Next();

            Assert.False(runner.IsActive);
            Assert.False(runner.IsChoice);
            Assert.Null(runner.CurrentOptions);
        }

        [Fact]
        public void RunRantTextProcessor()
        {
            var rantText = "one, two, three, four, five, six, seven, eight, nine, ten";

            var dialogTree = new DialogTree();
            dialogTree.AddNode(@"[numfmt:verbal][rep:10][sep:,\s]{[rn]}");

            var runner = new DialogTreeRunner(
                dialogTree,
                textProcessors: new ITextProcessor[] {
                    new RantTextProcessor()
                }
            );

            Assert.True(runner.IsActive);
            Assert.False(runner.IsChoice);
            Assert.Equal(rantText, runner.CurrentOptions[0]);

            runner.Next();

            Assert.False(runner.IsActive);
            Assert.False(runner.IsChoice);
            Assert.Null(runner.CurrentOptions);
        }
    }
}
