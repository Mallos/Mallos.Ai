namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;

    public class DialogTree : IDialogTree
    {
        public Guid RootNode { get; private set; }

        private readonly Dictionary<Guid, DialogEntity> nodes = new Dictionary<Guid, DialogEntity>();
        private readonly Dictionary<Guid, List<Guid>> links = new Dictionary<Guid, List<Guid>>();
        private readonly Dictionary<Guid, Dictionary<string, object>> properties = new Dictionary<Guid, Dictionary<string, object>>();

        public IReadOnlyDictionary<string, object> GetProperties(Guid nodeKey)
        {
            properties.TryGetValue(nodeKey, out var value);
            return value;
        }

        public void AddProperty(Guid nodeKey, string key, object value)
        {
            if (!properties.ContainsKey(nodeKey))
            {
                properties[nodeKey] = new Dictionary<string, object>();
            }

            properties[nodeKey][key] = value;
        }

        public DialogEntity GetNode(Guid nodeKey)
        {
            nodes.TryGetValue(nodeKey, out var value);
            return value;
        }

        public Guid AddNode(string text)
        {
            return AddNewNode(new DialogEntity(false, text));
        }

        public Guid AddChoice(string text)
        {
            return AddNewNode(new DialogEntity(true, text));
        }

        public Guid[] GetLinks(Guid nodeKey)
        {
            if (links.ContainsKey(nodeKey))
            {
                return links[nodeKey].ToArray();
            }

            return null;
        }

        public void AddLink(Guid nodeKey, params Guid[] nodes)
        {
            if (links.ContainsKey(nodeKey))
            {
                links[nodeKey].AddRange(nodes);
            }
            else
            {
                links[nodeKey] = new List<Guid>(nodes);
            }
        }

        private Guid AddNewNode(DialogEntity entity)
        {
            var key = Guid.NewGuid();

            if (nodes.Count == 0)
            {
                this.RootNode = key;
            }

            nodes[key] = entity;
            return key;
        }
    }
}
