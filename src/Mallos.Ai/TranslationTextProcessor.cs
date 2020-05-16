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

        /// <inheritdoc />
        public string Process(string text, Blackboard blackboard, IReadOnlyDictionary<string, object> properties = null)
        {
            if (this.Dictionary.TryGetValue(text, out string value))
            {
                return value;
            }

            return text;
        }
    }
}
