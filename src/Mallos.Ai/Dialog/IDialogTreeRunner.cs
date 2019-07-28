namespace Mallos.Ai.Dialog
{
    using System;

    /// <summary>
    /// An interface that gives a good overview of a dialog tree runner.
    /// Which is used for running dialog trees.
    /// </summary>
    public interface IDialogTreeRunner
    {
        /// <summary>
        /// Gets the source <see cref="DialogTree"/>.
        /// </summary>
        DialogTree Source { get; }

        /// <summary>
        /// Gets wether this is initialized.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets wether it is still active or not started.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets the active entities in this dialog.
        /// </summary>
        /// // FIXME: Give more information to the dialog tree
        // List<Blackboard> ActiveEntities { get; }

        /// <summary>
        /// Gets the current message state.
        /// </summary>
        DialogState State { get; }

        /// <summary>
        /// Move to the next node.
        /// </summary>
        /// <param name="key">The choice key.</param>
        /// <param name="blackboard">The acting entity blackboard.</param>
        /// <returns>true; if successful, otherwise false.</returns>
        bool Next(Guid? key = null, Blackboard blackboard = null);
    }
}
