namespace Mallos.Ai.Dialog
{
    using System;
    using System.Collections.Generic;

    public interface IDialogTree
    {
        Guid RootNode { get; }

        IReadOnlyDictionary<string, object> GetProperties(Guid nodeKey);

        void AddProperty(Guid nodeKey, string key, object value);

        DialogEntity GetNode(Guid nodeKey);

        Guid AddNode(string text);

        Guid AddChoice(string text);

        Guid[] GetLinks(Guid nodeKey);

        void AddLink(Guid nodeKey, params Guid[] nodes);
    }
}
