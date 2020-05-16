namespace Mallos.Ai.Dialog
{
    using System;

    /// <summary>
    /// An abstract class for handling the dialog history.
    /// </summary>
    public abstract class DialogHistoryCollection
    {
        /// <summary>
        /// Returns if the node key have been called before.
        /// </summary>
        /// <param name="dialogKey">The dialog key.</param>
        /// <param name="nodeKey">The dialog node key.</param>
        /// <returns>true, if the node has been called before; otherwise, false.</returns>
        public abstract bool IsNodeVisited(Guid dialogKey, Guid nodeKey);

        /// <summary>
        /// Called when a new node is called.
        /// </summary>
        /// <param name="dialogKey">The dialog key.</param>
        /// <param name="nodeKey">The dialog node key.</param>
        public abstract void UpdateVisitedNode(Guid dialogKey, Guid nodeKey);
    }
}
