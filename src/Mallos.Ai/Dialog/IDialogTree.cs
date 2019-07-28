namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An interface for exposing the main methods of a Dialog Tree.
    /// Dialog Tree is the storage of all the nodes and links etc.
    /// </summary>
    public interface IDialogTree
    {
        /// <summary>
        /// Gets the starting node in the tree.
        /// </summary>
        Guid RootNode { get; }

        /// <summary>
        /// Returns the properties a node has.
        /// </summary>
        /// <param name="nodeKey">The node key.</param>
        /// <returns>A dictionary of all the properties.</returns>
        IReadOnlyDictionary<string, object> GetProperties(Guid nodeKey);

        /// <summary>
        /// Add a property to the node.
        /// </summary>
        /// <param name="nodeKey">The node key.</param>
        /// <param name="key">The property key.</param>
        /// <param name="value">The property value.</param>
        void AddProperty(Guid nodeKey, string key, object value);

        /// <summary>
        /// Returns the node.
        /// </summary>
        /// <param name="nodeKey">The node key.</param>
        /// <returns>The node.</returns>
        DialogEntity GetNode(Guid nodeKey);

        /// <summary>
        /// Add a new NPC dialog node to the tree.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The node key.</returns>
        Guid AddNode(string text);

        /// <summary>
        /// Add a new user choice dialog node to the tree.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The node key.</returns>
        Guid AddChoice(string text);

        /// <summary>
        /// Returns the links a node has.
        /// </summary>
        /// <param name="nodeKey">The node key.</param>
        /// <returns>The node links keys.</returns>
        Guid[] GetLinks(Guid nodeKey);

        /// <summary>
        /// Add new links to a node.
        /// </summary>
        /// <param name="nodeKey">The node we are linking from.</param>
        /// <param name="nodes">The nodes we are linking to.</param>
        void AddLink(Guid nodeKey, params Guid[] nodes);
    }
}
