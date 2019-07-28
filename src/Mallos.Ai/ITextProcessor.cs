namespace Mallos.Ai
{
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
        /// <returns>The processed text.</returns>
        string Process(string text, Blackboard blackboard);
    }
}
