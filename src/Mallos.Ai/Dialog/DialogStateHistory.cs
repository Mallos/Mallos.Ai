namespace Mallos.Ai.Dialog
{
    using System;

    /// <summary>
    /// Represent the state of a dialog.
    /// </summary>
    public readonly struct DialogStateHistory
    {
        /// <summary>
        /// Gets the actor of this state.
        /// The person that forced the next state.
        /// </summary>
        public readonly Guid Acter;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public readonly DialogState State;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogStateHistory"/> struct.
        /// </summary>
        /// <param name="acter">The actor of this state.</param>
        /// <param name="state">The state.</param>
        public DialogStateHistory(
            Guid acter,
            DialogState state)
        {
            this.Acter = acter;
            this.State = state;
        }
    }
}
