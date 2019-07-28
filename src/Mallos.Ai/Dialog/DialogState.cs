namespace Mallos.Ai.Dialog
{
    using System;

    /// <summary>
    /// Represent the state of a dialog.
    /// </summary>
    public readonly struct DialogState
    {
        /// <summary>
        /// Gets if this is a choice.
        /// </summary>
        public readonly bool IsChoice;

        /// <summary>
        /// Gets the current sender of this message.
        /// </summary>
        public readonly Guid Sender;

        /// <summary>
        /// Gets the current node text.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Gets the current possible choices.
        /// </summary>
        public readonly DialogChoice[] Choices;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogState"/> struct.
        /// </summary>
        /// <param name="isChoice">if this is a choice.</param>
        /// <param name="sender">The sender of this message.</param>
        /// <param name="text">The text.</param>
        /// <param name="choices">The possible choices.</param>
        public DialogState(
            bool isChoice,
            Guid sender,
            string text,
            DialogChoice[] choices)
        {
            this.IsChoice = isChoice;
            this.Sender = sender;
            this.Text = text;
            this.Choices = choices;
        }
    }
}
