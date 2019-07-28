namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class DialogTreeRunner : IDialogTreeRunner
    {
        private bool initialized = false;

        public DialogTreeRunner(
            DialogTree source,
            ITextProcessor[] textProcessors = null)
        {
            this.IsActive = true;
            this.Source = source;
            this.TextProcessors = textProcessors;
        }

        public ITextProcessor[] TextProcessors { get; }

        public bool IsActive { get; private set; }

        public bool IsChoice => this.CurrentNode.IsChoice;

        public string CurrentText { get; private set; }

        public string[] CurrentOptions { get; private set; }

        protected DialogTree Source { get; }

        protected DialogEntity CurrentNode { get; private set; }

        protected Guid CurrentNodeKey { get; private set; }

        protected Guid[] CurrentOptionsKeys { get; private set; }

        public bool Next(int index = 0, Blackboard blackboard = null)
        {
            if (!this.initialized)
            {
                this.UpdateCurrent(this.Source.RootNode, blackboard);
                this.initialized = true;
                return true;
            }

            if (!this.IsActive)
            {
                // FIXME: Throw an exception here.
                return false;
            }

            if (index < 0 || index > CurrentOptionsKeys.Length)
            {
                // FIXME: Throw an exception here.
                return false;
            }

            var nextGuid = CurrentOptionsKeys[index];

            this.UpdateCurrent(nextGuid, blackboard);

            return true;
        }

        protected virtual void BeforeNext(Guid oldKey, Guid newKey)
        {
        }

        private void UpdateCurrent(Guid nodeKey, Blackboard blackboard)
        {
            this.BeforeNext(this.CurrentNodeKey, nodeKey);
            this.CurrentNodeKey = nodeKey;
            this.CurrentNode = this.Source.GetNode(nodeKey);
            this.CurrentText = ProcessEntityForText(this.CurrentNode, blackboard);

            this.CurrentOptionsKeys = this.Source.GetLinks(nodeKey);
            if (this.CurrentOptionsKeys == null)
            {
                this.IsActive = false;
                this.CurrentOptions = null;
            }
            else
            {
                var nodes = this.CurrentOptionsKeys.Select(key => this.Source.GetNode(key));

                this.ValidateNodes(nodes);

                this.CurrentOptions = nodes
                    .Select(node => ProcessEntityForText(node, blackboard))
                    .ToArray();
            }
        }

        private string ProcessEntityForText(DialogEntity entity, Blackboard blackboard)
        {
            if (this.TextProcessors != null && this.TextProcessors.Length > 0)
            {
                var result = entity.Text;
                foreach (var processor in this.TextProcessors)
                {
                    result = processor.Process(result, blackboard);
                }

                return result;
            }
            else
            {
                return entity.Text;
            }
        }

        [Conditional("DEBUG")]
        private void ValidateNodes(IEnumerable<DialogEntity> entities)
        {
            var first = entities.First();
            var valid = entities.Skip(1).All(entity => entity.IsChoice == first.IsChoice);

            if (!valid)
            {
                throw new InvalidOperationException("Mixing Nodes and Choices is not possible.");
            }
        }
    }
}
