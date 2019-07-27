namespace Mallos.Ai
{
    using System.Collections.Generic;
    using Xunit;

    public class TextProcessorTest
    {
        [Fact]
        public void Translate()
        {
            var text = "Hello";
            var textSwedish = "Hej";

            var dictionary = new Dictionary<string, string>
            {
                [text] = textSwedish
            };

            var processor = new TranslationTextProcessor(dictionary);
            var result = processor.Process(text, new Blackboard());

            Assert.Equal(textSwedish, result);
        }

        [Fact]
        public void RantProcess()
        {
            var rantCode = @"[numfmt:verbal][rep:10][sep:,\s]{[rn]}";
            var rantText = "one, two, three, four, five, six, seven, eight, nine, ten";

            var processor = new RantTextProcessor();
            var result = processor.Process(rantCode, new Blackboard());

            Assert.Equal(rantText, result);
        }

        [Fact(Skip = "This is not implemented yet.")]
        public void RantProcessWithVariables()
        {
            var variableText = "Hello world";

            var blackboard = new Blackboard();
            blackboard.Properties["text"] = variableText;

            var processor = new RantTextProcessor();
            var result = processor.Process(@"[v:text]", blackboard);

            Assert.Equal(variableText, result);
        }
    }
}
