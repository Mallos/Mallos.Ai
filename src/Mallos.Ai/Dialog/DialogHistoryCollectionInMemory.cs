namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// In Memory <see cref="DialogHistoryCollection"/>.
    /// </summary>
    public class DialogHistoryCollectionInMemory : DialogHistoryCollection
    {
        private readonly Dictionary<Guid, List<Guid>> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogHistoryCollectionInMemory"/> class.
        /// </summary>
        /// <param name="dictionary">If we want to reuse a old one.</param>
        public DialogHistoryCollectionInMemory(IDictionary<Guid, List<Guid>> dictionary = null)
        {
            if (dictionary != null)
            {
                this.collection = new Dictionary<Guid, List<Guid>>(dictionary);
            }
            else
            {
                this.collection = new Dictionary<Guid, List<Guid>>();
            }
        }

        /// <summary>
        /// Gets the current internal collection.
        /// </summary>
        public IReadOnlyDictionary<Guid, List<Guid>> Collection => this.collection;

        /// <inheritdoc />
        public override bool IsNodeVisited(Guid dialogKey, Guid nodeKey)
        {
            if (!this.collection.ContainsKey(dialogKey) ||
                this.collection[dialogKey] == null)
            {
                return false;
            }

            return this.collection[dialogKey].Contains(nodeKey);
        }

        /// <inheritdoc />
        public override void UpdateVisitedNode(Guid dialogKey, Guid nodeKey)
        {
            if (!this.collection.ContainsKey(dialogKey))
            {
                this.collection[dialogKey] = new List<Guid>();
            }

            if (!this.collection[dialogKey].Contains(nodeKey))
            {
                this.collection[dialogKey].Add(nodeKey);
            }
        }
    }
}
