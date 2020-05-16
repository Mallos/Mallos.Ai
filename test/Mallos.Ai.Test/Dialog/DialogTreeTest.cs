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

            Assert.True(runner.Next());

            Assert.True(runner.IsActive);
            Assert.False(runner.State.IsChoice);
            Assert.Equal(helloText, runner.State.Text);
            Assert.Equal(goodbyeText, runner.State.Choices[0].Text);

            Assert.True(runner.Next());

            Assert.False(runner.IsActive);
            Assert.True(runner.State.IsChoice);
            Assert.Equal(goodbyeText, runner.State.Text);
        }

        [Fact]
        public void RunNodesWithChoice()
        {
            var helloText = "Hello there! How are you?";
            var thankYouText = "I'm good thank you!";
            var goodbyeText = "Good, goodbye!";

            var dialogTree = new DialogTree();
            var node1 = dialogTree.AddNode(helloText);
            var node2 = dialogTree.AddChoice(thankYouText);
            var node3 = dialogTree.AddChoice(goodbyeText);
            dialogTree.AddLink(node1, node2, node3);

            var runner = new DialogTreeRunner(dialogTree);

            Assert.True(runner.Next());

            Assert.True(runner.IsActive);
            Assert.False(runner.State.IsChoice);
            Assert.Equal(helloText, runner.State.Text);
            Assert.Equal(thankYouText, runner.State.Choices[0].Text);
            Assert.Equal(goodbyeText, runner.State.Choices[1].Text);

            Assert.True(runner.Next(runner.State.Choices[1].Guid));

            Assert.False(runner.IsActive);
            Assert.True(runner.State.IsChoice);
            Assert.Equal(goodbyeText, runner.State.Text);
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

            Assert.True(runner.Next());

            Assert.False(runner.IsActive);
            Assert.False(runner.State.IsChoice);
            Assert.Equal(rantText, runner.State.Text);
        }
    }
}
