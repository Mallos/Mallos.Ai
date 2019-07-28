namespace Mallos.Ai.Dialog
{
    using System;

    /// <summary>
    /// Represent a choice which can be done by the player.
    /// </summary>
    public readonly struct DialogChoice
    {
        /// <summary>
        /// Gets the ID of this choice.
        /// </summary>
        public readonly Guid Guid;

        /// <summary>
        /// Gets wether this is assigned to a specific entity or not.
        /// </summary>
        public readonly Guid? AssignedTo;

        /// <summary>
        /// Gets the text for this option.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Gets wether this has been called before.
        /// </summary>
        public readonly bool CalledBefore;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogChoice"/> struct.
        /// </summary>
        /// <param name="guid">The ID of this choice.</param>
        /// <param name="assignedTo">Wether this is assigned to a specific entity or not.</param>
        /// <param name="text">The text for this option.</param>
        /// <param name="calledBefore">Wether this has been called before.</param>
        public DialogChoice(
            Guid guid,
            Guid? assignedTo,
            string text,
            bool calledBefore)
        {
            this.Guid = guid;
            this.AssignedTo = assignedTo;
            this.Text = text;
            this.CalledBefore = calledBefore;
        }
    }
}
