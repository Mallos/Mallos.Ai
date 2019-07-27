namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;

    public interface IDialogTree
    {
        void AddProperty(Guid nodeKey, string key, object value);

        Guid AddNode(string text);

        Guid AddChoice(string text);

        void AddLink(Guid node, params Guid[] nodes);
    }

    public class DialogTree : IDialogTree
    {
        private Dictionary<Guid, DialogEntity> nodes = new Dictionary<Guid, DialogEntity>();
        private Dictionary<Guid, List<Guid>> links = new Dictionary<Guid, List<Guid>>();

        public Guid AddNode(string text)
        {
            var key = Guid.New();
            nodes[key] = new DialogEntity(false, text);
            return key;
        }

        public Guid AddChoice(string text)
        {
            var key = Guid.New();
            nodes[key] = new DialogEntity(true, text);
            return key;
        }

        public void AddLink(Guid node, params Guid[] nodes)
        {
            if (links.ContainsKey(node))
            {
                links[node].Append(nodes);
            }
            else
            {
                links[node] = new List<Guid>(nodes);
            }
        }
    }
}
