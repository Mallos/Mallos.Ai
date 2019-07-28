namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    public class DialogTreeRunner : IDialogTreeRunner
    {
        private readonly List<DialogStateHistory> history = new List<DialogStateHistory>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogTreeRunner"/> class.
        /// </summary>
        /// <param name="source">the dialog tree source.</param>
        /// <param name="textProcessors">Tht text processors.</param>
        public DialogTreeRunner(
            DialogTree source,
            ITextProcessor[] textProcessors = null)
        {
            this.Source = source;
            this.TextProcessors = textProcessors;
        }

        /// <inheritdoc />
        public DialogTree Source { get; }

        /// <inheritdoc />
        public bool IsInitialized { get; private set; } = false;

        /// <inheritdoc />
        public bool IsActive { get; private set; } = true;

        /// <inheritdoc />
        public DialogState State { get; private set; }

        /// <summary>
        /// Gets the text processors.
        /// </summary>
        public ITextProcessor[] TextProcessors { get; }

        /// <summary>
        /// Gets the call history of this runner.
        /// </summary>
        /// <remarks>
        /// This is only used for the current runner to get retrospect for the current instance.
        /// </remarks>
        public IReadOnlyList<DialogStateHistory> History => history;

        /// <inheritdoc />
        public bool Next(Guid? key = null, Blackboard blackboard = null)
        {
            if (!this.IsInitialized)
            {
                this.UpdateCurrent(this.Source.RootNode, blackboard);
                this.IsInitialized = true;
                return true;
            }

            if (!this.IsActive)
            {
                throw new ArgumentException($"Runner is not active.");
            }

            if (this.State.Choices.Length == 0)
            {
                return false;
            }

            var choice = key.HasValue ? this.State.Choices.First(c => c.Guid == key) : this.State.Choices.First();
            this.UpdateCurrent(choice.Guid, blackboard);
            return true;
        }

        /// <summary>
        /// Called before we update to the state.
        /// </summary>
        /// <param name="oldState">The old state.</param>
        /// <param name="newState">The new state.</param>
        protected virtual void BeforeNext(DialogState oldState, DialogState newState)
        {
        }

        private void UpdateCurrent(Guid nodeKey, Blackboard blackboard)
        {
            var newNode = this.Source.GetNode(nodeKey);
            var newNodeLinks = this.Source.GetLinks(nodeKey);

            var newState = new DialogState(
                newNode.IsChoice,
                (blackboard != null) ? blackboard.Guid : Guid.Empty,
                this.ProcessEntityForText(nodeKey, newNode, blackboard),
                this.ProcessLinks(newNodeLinks, blackboard));

            this.BeforeNext(newState, this.State);

            if (blackboard != null)
            {
                blackboard.DialogHistoryCollection.UpdateVisitedNode(this.Source.Guid, nodeKey);
            }

            this.IsActive = newState.Choices.Length > 0;
            this.State = newState;
            this.history.Add(new DialogStateHistory(newState.Sender, this.State));
        }

        private DialogChoice[] ProcessLinks(Guid[] links, Blackboard blackboard)
        {
            if (links == null)
            {
                return new DialogChoice[0];
            }

            return links
                .Select(key =>
                {
                    var node = this.Source.GetNode(key);
                    var visited = HasBeenVisited(blackboard, key);

                    return new DialogChoice(
                        key,
                        null, // FIXME: Create Assigned to
                        ProcessEntityForText(key, node, blackboard),
                        visited);
                })
                .ToArray();
        }

        private bool HasBeenVisited(Blackboard blackboard, Guid nodeKey)
        {
            if (blackboard == null)
            {
                return false;
            }

            return blackboard.DialogHistoryCollection.IsNodeVisited(this.Source.Guid, nodeKey);
        }

        private string ProcessEntityForText(Guid nodeKey, DialogEntity entity, Blackboard blackboard)
        {
            if (this.TextProcessors != null && this.TextProcessors.Length > 0)
            {
                var props = this.Source.GetProperties(nodeKey);

                var result = entity.Text;
                foreach (var processor in this.TextProcessors)
                {
                    result = processor.Process(result, blackboard, props);
                }

                return result;
            }
            else
            {
                return entity.Text;
            }
        }
    }
}
