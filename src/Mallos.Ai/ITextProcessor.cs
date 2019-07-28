namespace Mallos.Ai
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for processing/transforming text.
    /// </summary>
    public interface ITextProcessor
    {
        /// <summary>
        /// Process the passed text and return the new result.
        /// </summary>
        /// <param name="text">Unprocessed text.</param>
        /// <param name="blackboard">The entity blackboard using this text.</param>
        /// <param name="properties">Custom properties.</param>
        /// <returns>The processed text.</returns>
        string Process(string text, Blackboard blackboard, IReadOnlyDictionary<string, object> properties = null);
    }
}
