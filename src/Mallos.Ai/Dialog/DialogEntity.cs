namespace Mallos.Ai.Dialog
{
    /// <summary>
    /// Represent a internal entity in the <see cref="DialogTree"/>.
    /// </summary>
    public readonly struct DialogEntity
    {
        /// <summary>
        /// Gets if this is a choice.
        /// </summary>
        public readonly bool IsChoice;

        /// <summary>
        /// Gets the text.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogEntity"/> struct.
        /// </summary>
        /// <param name="isChoice">If this is a choice.</param>
        /// <param name="text">The text.</param>
        public DialogEntity(
            bool isChoice,
            string text)
        {
            this.IsChoice = isChoice;
            this.Text = text;
        }
    }
}
