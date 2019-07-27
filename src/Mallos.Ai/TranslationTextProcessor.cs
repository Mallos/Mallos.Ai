namespace Mallos.Ai
{
    using System.Collections.Generic;

    public class TranslationTextProcessor : ITextProcessor
    {
        public Dictionary<string, string> Dictionary { get; set; }

        public TranslationTextProcessor(Dictionary<string, string> dictionary = null)
        {
            this.Dictionary = dictionary ?? new Dictionary<string, string>();
        }

        public string Process(string text, Blackboard blackboard)
        {
            if (this.Dictionary.TryGetValue(text, out string value))
            {
                return value;
            }

            return text;
        }
    }
}
